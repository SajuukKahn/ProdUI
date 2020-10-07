namespace ProdData.Factories
{
    using ProductionCore.Interfaces;
    using global::ProdData.Models;

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
