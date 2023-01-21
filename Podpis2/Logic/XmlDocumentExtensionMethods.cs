using System.Xml;

namespace Podpis2.Logic
{
    public static class XmlDocumentExtensionMethods
    {
        public static XmlDocument ToXmlDocument(this System.Text.StringBuilder stringBuilder)
        {
            string value = stringBuilder.ToString();
            return value.ToXmlDocument();
        }

        public static XmlDocument ToXmlDocument(this string value)
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(value);
            return xmlDocument;
        }

        public static string ToBase64(this System.Text.StringBuilder stringBuilder)
        {
            string s = stringBuilder.ToString();
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(s);
            return System.Convert.ToBase64String(bytes);
        }

        public static XmlNode GetFirstElementByTagName(this XmlDocument document, string tagName)
        {
            return document.GetElementsByTagName(tagName).Item(0);
        }
    }
}
