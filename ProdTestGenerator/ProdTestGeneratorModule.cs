namespace ProdTestGenerator
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProdTestGenerator.Services;
    using ProdTestGenerator.ViewModels;
    using ProdTestGenerator.Views;
    using ProductionCore.Interfaces;
    using ProductionCore.Interfaces.Services;

    /// <summary>
    /// Defines the <see cref="ProdTestGeneratorModule" />.
    /// </summary>
    [ModuleDependency(nameof(ProductionCore.ProductionCoreModule))]
    public class ProdTestGeneratorModule : IModule
    {
        /// <summary>
        /// Defines the _regionManager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdTestGeneratorModule"/> class.
        /// </summary>
        /// <param name="regionManager">The regionManager<see cref="IRegionManager"/>.</param>
        public ProdTestGeneratorModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// Gets or sets the FileService.
        /// </summary>
        public FileService? FileService { get; set; }

        /// <inheritdoc/>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.Regions["TestRegion"].Add(containerProvider.Resolve<ITestGeneratorView>());
            containerProvider.Resolve<IControllerService>();
        }

        /// <inheritdoc/>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ITestGeneratorView, TestGeneratorView>();
            containerRegistry.RegisterSingleton<IControllerService, ControllerService>();
            containerRegistry.Register<ITestGeneratorViewModel, TestGeneratorViewModel>();
            containerRegistry.RegisterSingleton<IFileService, FileService>();
        }
    }
}
