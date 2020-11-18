namespace ProdUI
{
    using System.Windows;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Unity;
    using ProdUI.Views;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App : PrismApplication
    {
        /// <inheritdoc/>
        protected override Window CreateShell()
        {
            return Container.Resolve<ProdUIView>();
        }

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
            base.OnInitialized();
            MaterialPalette.Palette.FontSizeS = 10;
            MaterialPalette.Palette.FontSize = 12;
            MaterialPalette.Palette.FontSizeL = 16;
        }
    }
}
