using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ProdData
{
    public class ProdData : IModule
    {
        private readonly IRegionManager _regionManager;

        public ProdData(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ProdDataRegion"];
            var view = containerProvider.Resolve<Views.ProdDataView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}