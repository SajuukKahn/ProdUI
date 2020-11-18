namespace ProdData
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProdData.Factories;
    using ProdData.Models;
    using ProdData.Services;
    using ProdData.ViewModels;
    using ProdData.Views;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProdDataModule" />.
    /// </summary>
    [ModuleDependency(nameof(ProductionCore.ProductionCoreModule))]
    public class ProdDataModule : IModule
    {
        /// <summary>
        /// Defines the _regionManager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdDataModule"/> class.
        /// </summary>
        /// <param name="regionManager">The regionManager<see cref="IRegionManager"/>.</param>
        public ProdDataModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <inheritdoc/>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.Regions["ProdDataRegion"].Add(containerProvider.Resolve<IProdDataView>());
            _regionManager.Regions["ProdModalRegion"].Add(containerProvider.Resolve<IProdModalDialogView>());
        }

        /// <inheritdoc/>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ICard, Card>();
            containerRegistry.Register<ICardSubStep, CardSubStep>();
            containerRegistry.Register<IChronometer, Chronometer>();
            containerRegistry.Register<IModalData, ModalData>();
            containerRegistry.Register<ICardFactory, CardFactory>();
            containerRegistry.Register<ICardSubStepFactory, CardSubStepFactory>();
            containerRegistry.Register<IChronometerFactory, ChronometerFactory>();
            containerRegistry.Register<IModalFactory, ModalFactory>();
            containerRegistry.Register<IProdDataView, ProdDataView>();
            containerRegistry.Register<IProdModalDialogView, ProdModalDialogView>();
            containerRegistry.RegisterSingleton<IModalService, ModalService>();
            containerRegistry.RegisterSingleton<IPlaybackService, PlaybackService>();
            containerRegistry.RegisterSingleton<IProdDataViewModel, ProdDataViewModel>();
            containerRegistry.RegisterSingleton<IProdModalDialogViewModel, ProdModalDialogViewModel>();
        }
    }
}
