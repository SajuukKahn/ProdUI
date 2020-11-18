﻿namespace ProdData
{
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProdData.Factories;
    using ProdData.Models;
    using ProdData.Services;
    using ProdData.ViewModels;
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
            containerRegistry.Register<ICard, Card>();
            containerRegistry.Register<ICardSubStep, CardSubStep>();
            containerRegistry.Register<IChronometer, Chronometer>();
            containerRegistry.Register<IModalData, ModalData>();
            containerRegistry.Register<ICardFactory, CardFactory>();
            containerRegistry.Register<ICardSubStepFactory, CardSubStepFactory>();
            containerRegistry.Register<IChronometerFactory, ChronometerFactory>();
            containerRegistry.Register<IModalFactory, ModalFactory>();
            containerRegistry.RegisterSingleton<IModalService, ModalService>();
            containerRegistry.RegisterSingleton<IPlaybackService, PlaybackService>();
            containerRegistry.RegisterSingleton<IProdDataViewModel, ProdDataViewModel>();
            containerRegistry.RegisterSingleton<IProdModalDialogViewModel, ProdModalDialogViewModel>();
        }
    }
}