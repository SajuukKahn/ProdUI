namespace ProdData.Factories
{
    using ProdData.Models;
    using ProdCore.Interfaces;

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
