using System.ComponentModel.Composition;
using Podpis2.Interfaces;

namespace Podpis2.Templates
{
    [Export(typeof(ITemplateProvider)), ExportMetadata("Template", "Detached")]
    public class DetachedTemplateProvider : TemplateProviderBase
    {
        private const string SignedDocumentTemplateName = "DetachedSignedDocumentTemplate";

        [ImportingConstructor]
        public DetachedTemplateProvider(ILocationManager locationManager) : base("DetachedSignedDocumentTemplate")
        {
        }
    }
}
