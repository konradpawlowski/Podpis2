using System.ComponentModel.Composition;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;
using Podpis2.Interfaces;
using System.Text;

namespace Podpis2.Logic
{
    [Export("EnvelopedXades", typeof(IDocument)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class XadesDocument : IDocument
    {
        protected const string TemplateMetadata = "Template";

        private const string SignatureValueAlgorithm = "SHA1";

        private readonly ICertificateManager certificateManager;

        //private readonly Lazy<System.Text.StringBuilder> templateStringBuilderLazy;


        [ImportMany]
        public Lazy<ITemplateProvider, System.Collections.Generic.IDictionary<string, object>>[] TemplateProviders
        {
            get;
            set;
        }

        public  System.Text.StringBuilder TemplateStringBuilder
        {
            //get
            //{
            //    return this.templateStringBuilderLazy.Value;
            //}
            get; set;
        }

        [ImportingConstructor]
        public XadesDocument(ICertificateManager certificateManager)
        {
            if (certificateManager == null)
            {
                throw new System.ArgumentNullException("certificateManager");
            }
            this.certificateManager = certificateManager;
           // this.templateStringBuilderLazy = new Lazy<System.Text.StringBuilder>(new Func<System.Text.StringBuilder>(this.GetTemplateStringBuilder));
        }

        public virtual void AddDocument(string document)
        {
            this.TemplateStringBuilder.Replace("{document}", document);
        }

        public string Sign()
        {
            this.AddCertificate(this.TemplateStringBuilder);
            XmlDocument xmlDocument = this.TemplateStringBuilder.ToXmlDocument();
            this.SignAndAddReferences(xmlDocument);
            xmlDocument = this.TemplateStringBuilder.ToXmlDocument();
            this.AddSignatureValue(this.TemplateStringBuilder, xmlDocument);
            return this.TemplateStringBuilder.ToBase64();
        }

        protected virtual void SignAndAddReferences(XmlDocument xmlDocument)
        {
            this.AddSignedPropertiesReference(this.TemplateStringBuilder, xmlDocument, "xades:SignedProperties", "{signed-properties-reference}");
            this.AddSignedPropertiesReference(this.TemplateStringBuilder, xmlDocument, "ds:Object", "{document-reference}");
        }

        protected virtual bool IsSearchedMetadata(System.Collections.Generic.IDictionary<string, object> metadata)
        {
            return metadata.ContainsKey("Template") && string.Equals((string)metadata["Template"], "Enveloping", System.StringComparison.OrdinalIgnoreCase);
        }
        public static byte[] HashAndSignBytes(byte[] DataToSign, RSAParameters Key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(Key); 

                // Hash and sign the data. Pass a new instance of SHA256
                // to specify the hashing algorithm.
                return RSAalg.SignData(DataToSign, SHA1.Create());
            }
            catch(CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }
        }
        private void AddSignatureValue(System.Text.StringBuilder stringBuilder, XmlDocument document)
        {
            RSA privateKey = this.certificateManager.GetRSACryptoServiceProvider();
               
            XmlNode firstElementByTagName = document.GetFirstElementByTagName("ds:SignedInfo");
            //
            // byte[] originalData = Encoding.UTF8.GetBytes(firstElementByTagName.OuterXml);
            // byte[] signedData;
            //
            // signedData = HashAndSignBytes(originalData, privateKey.ExportParameters(true));
            using (System.IO.Stream stream = XadesDocument.CanonicalizeNode(firstElementByTagName))
            {
                string halg = System.Security.Cryptography.CryptoConfig.MapNameToOID("SHA1");
               //  byte[] inArray = privateKey.SignData(stream, halg);
               byte[] inArray = privateKey.SignData(stream, HashAlgorithmName.SHA1, RSASignaturePadding.Pkcs1);
                string newValue = System.Convert.ToBase64String(inArray);
                stringBuilder.Replace("{signature-value}", newValue);
            }
        }

        private void AddCertificate(System.Text.StringBuilder stringBuilder)
        {
            stringBuilder.Replace("{certificate}", this.certificateManager.GetContents());
            stringBuilder.Replace("{certificate-digest}", this.certificateManager.GetSha1DigestValue());
            stringBuilder.Replace("{certificate-issuer}", this.certificateManager.GetIssuer());
            stringBuilder.Replace("{certificate-serial}", this.certificateManager.GetSerialNumber());
        }

        protected void AddSignedPropertiesReference(System.Text.StringBuilder stringBuilder, XmlDocument document, string tagName, string placeholder)
        {
            XmlNode firstElementByTagName = document.GetFirstElementByTagName(tagName);
            using (System.IO.Stream stream = XadesDocument.CanonicalizeNode(firstElementByTagName))
            {
                System.Security.Cryptography.SHA1 sHA = System.Security.Cryptography.SHA1.Create();
                byte[] inArray = sHA.ComputeHash(stream);
                string newValue = System.Convert.ToBase64String(inArray);
                stringBuilder.Replace(placeholder, newValue);
            }
        }

        private static System.IO.Stream CanonicalizeNode(XmlNode node)
        {
            System.IO.Stream result;
            using (XmlNodeReader xmlNodeReader = new XmlNodeReader(node))
            {
                using (System.IO.Stream stream = new System.IO.MemoryStream())
                {
                    using (XmlWriter xmlWriter = new XmlTextWriter(stream, System.Text.Encoding.UTF8))
                    {
                        xmlWriter.WriteNode(xmlNodeReader, false);
                        xmlWriter.Flush();
                        stream.Position = 0L;
                        XmlDsigC14NTransform xmlDsigC14NTransform = new XmlDsigC14NTransform();
                        xmlDsigC14NTransform.LoadInput(stream);
                        result = (System.IO.Stream)xmlDsigC14NTransform.GetOutput();
                    }
                }
            }
            return result;
        }

        private System.Text.StringBuilder GetTemplateStringBuilder()
        {
            Lazy<ITemplateProvider, System.Collections.Generic.IDictionary<string, object>>[] templateProviders = this.TemplateProviders;
            for (int i = 0; i < templateProviders.Length; i++)
            {
                Lazy<ITemplateProvider, System.Collections.Generic.IDictionary<string, object>> lazy = templateProviders[i];
                if (this.IsSearchedMetadata(lazy.Metadata))
                {
                    string template = lazy.Value.Template;
                    return new System.Text.StringBuilder(template);
                }
            }
            throw new System.InvalidOperationException("The expected template provider hasn't been found.");
        }
    }
}
