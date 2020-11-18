namespace ProductionCore
{
    using Prism.Ioc;
    using Prism.Modularity;
    using ProductionCore.Interfaces;
    using ProductionCore.Services;

    /// <summary>
    /// Defines the <see cref="ProductionCoreModule" />.
    /// </summary>
    public class ProductionCoreModule : IModule
    {
        /// <summary>
        /// The OnInitialized.
        /// </summary>
        /// <param name="containerProvider">The containerProvider<see cref="IContainerProvider"/>.</param>
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        /// <summary>
        /// The RegisterTypes.
        /// </summary>
        /// <param name="containerRegistry">The containerRegistry<see cref="IContainerRegistry"/>.</param>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMediationService, MediationService>();
        }
    }
}
