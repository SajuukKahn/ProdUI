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
        /// <summary>
        /// The CreateShell.
        /// </summary>
        /// <returns>The <see cref="Window"/>.</returns>
        protected override Window CreateShell()
        {
            return Container.Resolve<ProdUIView>();
        }

        /// <summary>
        /// The RegisterTypes.
        /// </summary>
        /// <param name="containerRegistry">The containerRegistry<see cref="IContainerRegistry"/>.</param>
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            MaterialPalette.Palette.FontSizeS = 10;
            MaterialPalette.Palette.FontSize = 12;
            MaterialPalette.Palette.FontSizeL = 16;
        }

        /// <summary>
        /// The CreateModuleCatalog.
        /// </summary>
        /// <returns>The <see cref="IModuleCatalog"/>.</returns>
        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
