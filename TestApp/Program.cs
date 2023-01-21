// See https://aka.ms/new-console-template for more information

using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;
using Podpis2.Logic;
using Podpis2.Templates;

var thumbPrint = "7b8e8e9407363622a0e0b1c7c21f3f19eaf69499";

CertificateManager cm = new CertificateManager();
X509Certificate2 cert = cm.GetCertificatesByThumbprint(thumbPrint);

XmlDocument xmlDoc = new()
{
    // Load an XML file into the XmlDocument object.
    PreserveWhitespace = true
};
xmlDoc.Load("test.xml");

CmsDocument cms = new CmsDocument(cm);

EnvelopingTemplateProvider prov = new EnvelopingTemplateProvider();

XadesDocument xs = new XadesDocument(cm);
xs.TemplateStringBuilder = new StringBuilder(prov.Template);
xs.AddDocument(xmlDoc.LastChild.OuterXml);


var _signedBase64 = xs.Sign();
                
//xs.TemplateStringBuilder.ToXmlDocument().Save(@"c:\temp\podpisany.xml");

var SignedXmlOuterText = xs.TemplateStringBuilder.ToXmlDocument().OuterXml;