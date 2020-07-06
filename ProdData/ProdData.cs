using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;

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
            IRegion region = _regionManager.Regions["MessageListRegion"];
            var view = containerProvider.Resolve<Views.ProdDataView>();
            region.Add(view);
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            throw new NotImplementedException();
        }
    }
}
