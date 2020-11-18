namespace ProdData.Factories
{
    using ProdData.Models;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
    public class ModalFactory : IModalFactory
    {
        /// <inheritdoc/>
        public IModalData CreateModalData()
        {
            return new ModalData();
        }
    }
}
