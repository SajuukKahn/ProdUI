using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using ProdUI.Views;
using System.Windows;
using Telerik.Windows.Controls;

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
            MaterialPalette.Palette.FontSizeS = 10;
            MaterialPalette.Palette.FontSize = 12;
            MaterialPalette.Palette.FontSizeL = 16;
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}