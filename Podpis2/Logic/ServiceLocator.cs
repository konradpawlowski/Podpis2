using System.ComponentModel.Composition.Hosting;

namespace Podpis2.Logic
{
    public static class ServiceLocator
    {
        private static readonly CompositionContainer Container;

        static ServiceLocator()
        {
            DirectoryCatalog directoryCatalog = new DirectoryCatalog(".", "*.dll");
            DirectoryCatalog directoryCatalog2 = new DirectoryCatalog(".", "*.exe");
            AggregateCatalog catalog = new AggregateCatalog(new DirectoryCatalog[]
            {
                directoryCatalog,
                directoryCatalog2
            });
            ServiceLocator.Container = new CompositionContainer(catalog, new ExportProvider[0]);
        }

        public static T GetInstance<T>()
        {
            return ServiceLocator.Container.GetExportedValue<T>();
        }

        public static T GetInstance<T>(string contractName)
        {
            return ServiceLocator.Container.GetExportedValue<T>(contractName);
        }
    }
}