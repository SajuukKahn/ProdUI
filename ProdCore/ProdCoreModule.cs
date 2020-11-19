namespace ProdCore
{
    using Prism.Ioc;
    using Prism.Modularity;
    using ProdCore.Interfaces;
    using ProdCore.Services;

    /// <summary>
    /// Defines the <see cref="ProdCoreModule" />.
    /// </summary>
    public class ProdCoreModule : IModule
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
