namespace ProdProgramSelect
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProdProgramSelect.Factories;
    using ProdProgramSelect.Models;
    using ProdProgramSelect.Services;
    using ProdProgramSelect.ViewModels;
    using ProdProgramSelect.Views;
    using ProdCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProdProgramSelectModule" />.
    /// </summary>
    [ModuleDependency(nameof(ProdCore.ProdCoreModule))]
    [ModuleDependency("ProdTestGeneratorModule")]
    public class ProdProgramSelectModule : IModule
    {
        /// <summary>
        /// Defines the _regionManager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdProgramSelectModule"/> class.
        /// </summary>
        /// <param name="regionManager">The regionManager<see cref="IRegionManager"/>.</param>
        public ProdProgramSelectModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <inheritdoc/>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.Regions["ShellProgramSelectRegion"].Add(containerProvider.Resolve<IProgramSelectView>());
        }

        /// <inheritdoc/>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IBarcode, Barcode>();
            containerRegistry.Register<IProgramData, ProgramData>();
            containerRegistry.Register<IBarcodeFactory, BarcodeFactory>();
            containerRegistry.Register<IProgramDataFactory, ProgramDataFactory>();
            containerRegistry.Register<IProgramSelectView, ProgramSelectView>();
            containerRegistry.RegisterSingleton<IProgramDataService, ProgramDataService>();
            containerRegistry.RegisterSingleton<IProgramSelectViewModel, ProgramSelectViewModel>();
        }
    }
}
