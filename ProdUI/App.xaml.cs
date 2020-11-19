namespace ProdUI
{
    using System.Windows;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using Prism.Unity;
    using ProdUI.Views;
    using Telerik.Windows.Controls;

    /// <inheritdoc/>
    public partial class App : PrismApplication
    {

        /// <inheritdoc/>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
        }

        /// <inheritdoc/>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }

        /// <inheritdoc/>
        protected override void OnInitialized()
        {
            Window shell;

            shell = Container.Resolve<ProdUIView>();

            shell.Show();
            MainWindow = shell;

            RegionManager.SetRegionManager(MainWindow, Container.Resolve<IRegionManager>());
            RegionManager.UpdateRegions();

            base.OnInitialized();
            MaterialPalette.Palette.FontSizeS = 10;
            MaterialPalette.Palette.FontSize = 12;
            MaterialPalette.Palette.FontSizeL = 16;
        }

        protected override Window CreateShell()
        {
            return null;
        }
    }
}
