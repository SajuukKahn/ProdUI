using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductionCore
{
    public class ProductionCore : IModule
    {
        //TODO Does this need anything in it?  Everything works fine with this blank, but does it make sense to resolve types in here from interfaces?
        public void OnInitialized(IContainerProvider containerProvider)
        {
            
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }
    }
}
