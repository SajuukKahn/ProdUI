using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ProdTestGenerator.ViewModels;
using ProdTestGenerator.Views;

namespace ProdTestGenerator
{
    public class ProdTestGenerator : IModule
    {
        private readonly IRegionManager _regionManager;

        public ProdTestGenerator(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        public void OnInitialized(IContainerProvider containerProvider)
        {
            containerProvider.Resolve<TestGeneratorSingleton>();
            IRegion region = _regionManager.Regions["TestRegion"];
            var view = containerProvider.Resolve<TestGeneratorView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<TestGeneratorSingleton>();
        }
    }
}