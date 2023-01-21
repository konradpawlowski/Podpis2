using System.ComponentModel.Composition;
using Podpis2.Interfaces;

namespace Podpis2.Templates
{
    [Export(typeof(ITemplateProvider)), ExportMetadata("Template", "Enveloping")]
    public class EnvelopingTemplateProvider : TemplateProviderBase
    {
        private const string SignedDocumentTemplateName = "SignedDocumentTemplate";

        [ImportingConstructor]
        public EnvelopingTemplateProvider() : base( "SignedDocumentTemplate")
        {
        }
    }
}
