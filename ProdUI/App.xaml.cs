using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;
using ProdUI.Views;
using System.Windows;
using System.Windows.Controls;

namespace ProdUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<ProdUIView>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            
        }

        protected override void ConfigureRegionAdapterMappings(RegionAdapterMappings regionAdapterMappings)
        {
        //    base.ConfigureRegionAdapterMappings(regionAdapterMappings);
        //    regionAdapterMappings.RegisterMapping(typeof(StackPanel), Container.Resolve<StackPanelRegionAdapter>());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        { 
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

    }
}
