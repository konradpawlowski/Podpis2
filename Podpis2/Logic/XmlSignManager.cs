using System.ComponentModel.Composition;
using Podpis2.Interfaces;

namespace Podpis2.Logic
{
    [Export(typeof(IXmlSignManager))]
    public class XmlSignManager : IXmlSignManager
    {
        public string ContractName
        {
            get;
            set;
        }

        public System.Collections.Generic.IEnumerable<string> Sign(System.Collections.Generic.IEnumerable<string> documents)
        {
            if (documents == null)
            {
                throw new System.ArgumentNullException("documents");
            }
            return documents.Select(new Func<string, string>(this.Sign)).ToArray<string>();
        }

        private string Sign(string document)
        {
            IDocument instance = ServiceLocator.GetInstance<IDocument>(this.ContractName);
            instance.AddDocument(document);
            return instance.Sign();
        }
    }
}
