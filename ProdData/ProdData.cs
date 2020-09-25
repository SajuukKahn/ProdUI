namespace ProdData
{
    using global::ProdData.ViewModels;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProdData" />.
    /// </summary>
    [ModuleDependency("ProdProgramSelect")]
    public class ProdData : IModule
    {
        /// <summary>
        /// Defines the _regionManager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdData"/> class.
        /// </summary>
        /// <param name="regionManager">The regionManager<see cref="IRegionManager"/>.</param>
        public ProdData(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// The OnInitialized.
        /// </summary>
        /// <param name="containerProvider">The containerProvider<see cref="IContainerProvider"/>.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ProdDataRegion"];
            var view1 = containerProvider.Resolve<Views.ProdDataView>();
            region.Add(view1);

            region = _regionManager.Regions["ProdModalRegion"];
            var view3 = containerProvider.Resolve<Views.ProdModalDialogView>();
            region.Add(view3);
        }

        /// <summary>
        /// The RegisterTypes.
        /// </summary>
        /// <param name="containerRegistry">The containerRegistry<see cref="IContainerRegistry"/>.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IProdDataViewModel, ProdDataViewModel>();
        }
    }
}
