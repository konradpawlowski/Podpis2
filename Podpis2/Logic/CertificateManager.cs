using System.ComponentModel.Composition;
using System.Numerics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Podpis2.Interfaces;

namespace Podpis2.Logic
{
    [Export(typeof(ICertificateManager)), PartCreationPolicy(CreationPolicy.Shared)]
    public class CertificateManager : ICertificateManager
    {
        private const string GetAllCertificatesUserName = "*";

        private X509Certificate2 certificate;

        public bool HasCertificateByCn(string cn)
        {
            return this.GetCertificatesByCn(cn).Any<X509Certificate2>();
        }

        public RSACryptoServiceProvider GetRSACryptoServiceProvider()
        {
            
            // (certificate.PrivateKey as RSACng)?.Key.SetProperty(
            //     new CngProperty(
            //         "Export Policy",
            //         BitConverter.GetBytes((int)CngExportPolicies.AllowPlaintextExport),
            //         CngPropertyOptions.Persist));
            RSA rsa = (RSA)certificate.PrivateKey;
            var cng = (certificate.GetRSAPrivateKey() as RSACng);
            var uiPolicy = new CngUIPolicy(CngUIProtectionLevels.ProtectKey, "Protect");
            
            cng.Key.SetProperty(  new CngProperty(
                "UIPolicy",
                BitConverter.GetBytes((int)CngUIProtectionLevels.ProtectKey),
                CngPropertyOptions.Persist));
            
            
            (certificate.PrivateKey as RSACng).Key.SetProperty(
                
                new CngProperty(
                    "ExportPolicy",
                    BitConverter.GetBytes((int)CngExportPolicies.AllowPlaintextExport),
                    CngPropertyOptions.Persist));

            RSAParameters RSAParameters = rsa.ExportParameters(true);                      

        
            
            
            return (RSACryptoServiceProvider) certificate.GetRSAPrivateKey();
        }

        public System.Collections.Generic.IEnumerable<X509Certificate2> GetCertificatesByCn(string cn)
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadOnly);
            System.DateTime dateTime = System.DateTime.Now;
            return from X509Certificate2 cert in x509Store.Certificates
                   where this.CertificateConformsToCriteria(cn, cert, dateTime)
                   select cert;
        }

        public X509Certificate2 GetCertificatesByThumbprint(string thumb)
        {
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            x509Store.Open(OpenFlags.ReadOnly);
            System.DateTime dateTime = System.DateTime.Now;
            certificate = x509Store.Certificates.Find(X509FindType.FindByThumbprint, thumb, false).First();
            return certificate;
        }


        public void SetChosenCertificate(X509Certificate2 certificateToSign)
        {
            this.certificate = certificateToSign;
        }

        public X509Certificate2 GetCertificate()
        {
            this.CheckIfCertificateChosenAndThrowException();
            return this.certificate;
        }

        public RSA GetPrivateKey()
        {
            this.CheckIfCertificateChosenAndThrowException();
            return this.certificate.GetRSAPrivateKey();
        }

        public string GetSha1DigestValue()
        {
            this.CheckIfCertificateChosenAndThrowException();
            byte[] rawData = this.certificate.RawData;
            System.Security.Cryptography.SHA1 sHA = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] inArray = sHA.ComputeHash(rawData);
            return System.Convert.ToBase64String(inArray);
        }

        public string GetIssuer()
        {
            this.CheckIfCertificateChosenAndThrowException();
            return this.certificate.Issuer;
        }

        public string GetSerialNumber()
        {
            this.CheckIfCertificateChosenAndThrowException();
            string value = this.certificate.SerialNumber ?? string.Empty;
            return BigInteger.Parse(value, System.Globalization.NumberStyles.HexNumber).ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        public string GetContents()
        {
            this.CheckIfCertificateChosenAndThrowException();
            return System.Convert.ToBase64String(this.certificate.RawData);
        }

        private bool CertificateConformsToCriteria(string cn, X509Certificate2 cert, System.DateTime dateTime)
        {
            return cert.HasPrivateKey && cert.NotAfter >= dateTime && this.MeetsSubjectNameCondition(cert, cn);
        }
        private bool CertificateConformsToCriteriaThumb(string thumb, X509Certificate2 cert, System.DateTime dateTime)
        {
            return cert.HasPrivateKey && cert.NotAfter >= dateTime && this.MeetsSubjectNameConditionThumb(cert, thumb);
        }


        private bool MeetsSubjectNameCondition(X509Certificate2 cert, string userName)
        {
            bool flag = string.Equals(userName, "*", System.StringComparison.OrdinalIgnoreCase);
            if (flag)
            {
                return true;
            }
            string toCheck = string.Format("CN={0}", userName);
            return cert.Subject.Contains(toCheck, StringComparison.OrdinalIgnoreCase);
            //return true;
        }
        private bool MeetsSubjectNameConditionThumb(X509Certificate2 cert, string thumb)
        {
         
            return cert.Thumbprint == thumb;
           
        }

        private void CheckIfCertificateChosenAndThrowException()
        {
            if (this.certificate == null)
            {
                throw new System.InvalidOperationException("No certificate chosen");
            }
        }
    }
}
