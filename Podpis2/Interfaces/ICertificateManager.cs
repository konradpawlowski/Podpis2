using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Podpis2.Interfaces
{
    public interface ICertificateManager
    {
        bool HasCertificateByCn(string cn);

        string GetSha1DigestValue();

        string GetIssuer();

        string GetSerialNumber();

        RSA GetPrivateKey();

        X509Certificate2 GetCertificate();

        string GetContents();

        RSACryptoServiceProvider GetRSACryptoServiceProvider();  
        System.Collections.Generic.IEnumerable<X509Certificate2> GetCertificatesByCn(string cn);

        void SetChosenCertificate(X509Certificate2 certificateToSign);
    }
}
