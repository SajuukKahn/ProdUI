using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ProdData.ViewModels;
using ProductionCore.Interfaces;

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
            var view1 = containerProvider.Resolve<Views.ProdDataView>();
            region.Add(view1);

            region = _regionManager.Regions["ProgramSelectRegion"];
            var view2 = containerProvider.Resolve<Views.ProgramSelectView>();
            region.Add(view2);

            region = _regionManager.Regions["ProdModalRegion"];
            var view3 = containerProvider.Resolve<Views.ProdModalDialogView>();
            region.Add(view3);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}