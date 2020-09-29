﻿namespace ProdProgramSelect
{
    using global::ProdProgramSelect.Factories;
    using global::ProdProgramSelect.Models;
    using global::ProdProgramSelect.ViewModels;
    using Prism.Ioc;
    using Prism.Modularity;
    using Prism.Regions;
    using ProductionCore.Interfaces;
    using ProductionCore.Services;

    /// <summary>
    /// Defines the <see cref="ProdProgramSelect" />.
    /// </summary>
    public class ProdProgramSelect : IModule
    {
        /// <summary>
        /// Defines the _regionManager.
        /// </summary>
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdProgramSelect"/> class.
        /// </summary>
        /// <param name="regionManager">The regionManager<see cref="IRegionManager"/>.</param>
        public ProdProgramSelect(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// The OnInitialized.
        /// </summary>
        /// <param name="containerProvider">The containerProvider<see cref="IContainerProvider"/>.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            IRegion region = _regionManager.Regions["ProgramSelectRegion"];
            var view = containerProvider.Resolve<Views.ProgramSelectView>();
            region.Add(view);
        }

        /// <summary>
        /// The RegisterTypes.
        /// </summary>
        /// <param name="containerRegistry">The containerRegistry<see cref="IContainerRegistry"/>.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IBarcode, Barcode>();
            containerRegistry.Register<IProgramData, ProgramData>();
            containerRegistry.RegisterSingleton<IBarcodeFactory, BarcodeFactory>();
            containerRegistry.RegisterSingleton<IProgramDataFactory, ProgramDataFactory>();
            containerRegistry.RegisterSingleton<IProgramSelectViewModel, ProgramSelectViewModel>();
            containerRegistry.RegisterSingleton<IProgramDataService, ProgramDataService>();
            containerRegistry.RegisterSingleton<IBarcodeService, BarcodeService>();
        }
    }
}
