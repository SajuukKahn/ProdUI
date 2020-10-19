namespace ProdTestGenerator
{
    using global::ProdTestGenerator.Services;
    using global::ProdTestGenerator.ViewModels;
    using global::ProdTestGenerator.Views;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProductionCore.Interfaces;
    using ProductionCore.Interfaces.Services;

    /// <summary>
    /// Defines the <see cref="ProdTestGenerator" />.
    /// </summary>
    [ModuleDependency("ProductionCore")]
    public class ProdTestGenerator : IModule
    {
        /// <summary>
        /// Defines the FileService.
        /// </summary>
        public FileService FileService;

        /// <summary>
        /// Defines the _regionManager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdTestGenerator"/> class.
        /// </summary>
        /// <param name="regionManager">The regionManager<see cref="IRegionManager"/>.</param>
        public ProdTestGenerator(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// The OnInitialized.
        /// </summary>
        /// <param name="containerProvider">The containerProvider<see cref="IContainerProvider"/>.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["TestRegion"];
            var view = containerProvider.Resolve<TestGeneratorView>();
            region.Add(view);
        }

        /// <summary>
        /// The RegisterTypes.
        /// </summary>
        /// <param name="containerRegistry">The containerRegistry<see cref="IContainerRegistry"/>.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IControllerService, ControllerService>();
            containerRegistry.Register<ITestGeneratorViewModel, TestGeneratorViewModel>();
            containerRegistry.RegisterSingleton<IFileService, FileService>();
        }
    }
}
