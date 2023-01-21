using System.ComponentModel.Composition;
using System.Xml;
using Podpis2.Interfaces;

namespace Podpis2.Logic
{
    [Export("DetachedXades", typeof(IDocument)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class DetachedXadesDocument : XadesDocument
    {
        private string detachedFile;

        [ImportingConstructor]
        public DetachedXadesDocument(ICertificateManager certificateManager) : base(certificateManager)
        {
        }

        public override void AddDocument(string document)
        {
            if (string.IsNullOrEmpty(document))
            {
                throw new System.ArgumentException("Document cannot be empty.", "document");
            }
            this.detachedFile = document;
        }

        protected override void SignAndAddReferences(XmlDocument xmlDocument)
        {
            base.AddSignedPropertiesReference(base.TemplateStringBuilder, xmlDocument, "xades:SignedProperties", "{signed-properties-reference}");
            this.AddSignedPropertiesReferenceForDocument(base.TemplateStringBuilder, "{document-reference}");
        }

        protected override bool IsSearchedMetadata(System.Collections.Generic.IDictionary<string, object> metadata)
        {
            return metadata.ContainsKey("Template") && string.Equals((string)metadata["Template"], "Detached", System.StringComparison.OrdinalIgnoreCase);
        }

        private void AddSignedPropertiesReferenceForDocument(System.Text.StringBuilder stringBuilder, string placeholder)
        {
            using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream())
            {
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(memoryStream))
                {
                    streamWriter.Write(this.detachedFile);
                }
                memoryStream.Position = 0L;
                System.Security.Cryptography.SHA1 sHA = System.Security.Cryptography.SHA1.Create();
                byte[] inArray = sHA.ComputeHash(memoryStream);
                string newValue = System.Convert.ToBase64String(inArray);
                stringBuilder.Replace(placeholder, newValue);
            }
        }
    }
}
