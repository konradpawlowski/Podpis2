
namespace Podpis2.Interfaces
{
    public interface IDocument
    {
        void AddDocument(string document);

        string Sign();
    }
}
