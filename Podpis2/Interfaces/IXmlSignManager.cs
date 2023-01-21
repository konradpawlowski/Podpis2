namespace Podpis2.Interfaces
{
    public interface IXmlSignManager
    {
        string ContractName
        {
            get;
            set;
        }

        System.Collections.Generic.IEnumerable<string> Sign(System.Collections.Generic.IEnumerable<string> documents);
    }
}
