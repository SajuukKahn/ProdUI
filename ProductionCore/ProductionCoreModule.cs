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
        /// <inheritdoc/>
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        /// <inheritdoc/>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IMediationService, MediationService>();
        }
    }
}
