using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using ProdCards.Views;

namespace ProdCards
{
    public class ProdCards : IModule
    {
        private readonly IRegionManager _regionManager;
        public ProdCards(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
        
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["MessageInputRegion"];
            var view = containerProvider.Resolve<Views.ProdCardsView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }
    }
}
