namespace ProductionCore
{
    using global::ProductionCore.Interfaces;
    using global::ProductionCore.Services;
    using Prism.Ioc;
    using Prism.Modularity;

    /// <summary>
    /// Defines the <see cref="ProductionCore" />.
    /// </summary>
    public class ProductionCore : IModule
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
