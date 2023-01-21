using System.ComponentModel.Composition;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using Podpis2.Interfaces;

namespace Podpis2.Logic
{
    [Export("Cms", typeof(IDocument)), PartCreationPolicy(CreationPolicy.NonShared)]
    public class CmsDocument : IDocument
    {
        private readonly ICertificateManager certificateManager;

        private string documentToSign;

        [ImportingConstructor]
        public CmsDocument(ICertificateManager certificateManager)
        {
            if (certificateManager == null)
            {
                throw new System.ArgumentNullException("certificateManager");
            }
            this.certificateManager = certificateManager;
        }

        public void AddDocument(string document)
        {
            if (string.IsNullOrEmpty(document))
            {
                throw new System.ArgumentException("Document cannot be empty", "document");
            }
            this.documentToSign = document;
        }

        public string Sign()
        {
            System.Text.Encoding unicode = System.Text.Encoding.Unicode;
            byte[] bytes = unicode.GetBytes(this.documentToSign);
            ContentInfo contentInfo = new ContentInfo(bytes);
            bool detached = true;
            SignedCms signedCms = new SignedCms(contentInfo, detached);
            X509Certificate2 certificate = this.certificateManager.GetCertificate();
            CmsSigner signer = new CmsSigner(certificate)
            {
                IncludeOption = X509IncludeOption.EndCertOnly
            };
            SignedCms arg_50_0 = signedCms;
            bool silent = false;
            arg_50_0.ComputeSignature(signer, silent);
            byte[] inArray = signedCms.Encode();
            return System.Convert.ToBase64String(inArray);
        }
    }
}
