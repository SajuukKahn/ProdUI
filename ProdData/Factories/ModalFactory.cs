namespace ProdData.Factories
{
    using global::ProdData.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ModalFactory" />.
    /// </summary>
    public class ModalFactory : IModalFactory
    {
        /// <summary>
        /// The CreateModalData.
        /// </summary>
        /// <returns>The <see cref="IModalData"/>.</returns>
        public IModalData CreateModalData()
        {
            return new ModalData();
        }
    }
}
