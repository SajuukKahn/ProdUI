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
    [ModuleDependency("ProductionCore")]
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
            containerRegistry.RegisterSingleton<IControllerService, ControllerService>(); // MAC: Controllerservice - why remove?
            containerRegistry.Register<ITestGeneratorViewModel, TestGeneratorViewModel>();
            containerRegistry.RegisterSingleton<IFileService, FileService>();
        }
    }
}
