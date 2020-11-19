namespace ProdCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IModalService" />.
    /// </summary>
    public interface IModalService
    {
        /// <summary>
        /// Gets a value indicating whether ModalActive.
        /// </summary>
        bool ModalActive { get; }

        /// <summary>
        /// Gets or sets the ActiveModalData.
        /// </summary>
        IModalData ActiveModalData { get; set; }

        /// <summary>
        /// The ShowModal.
        /// </summary>
        /// <param name="modalData">The modalData<see cref="IModalData"/>.</param>
        void ShowModal(IModalData modalData);

        /// <summary>
        /// The CloseModal.
        /// </summary>
        void CloseModal();

        /// <summary>
        /// The CreateModalData.
        /// </summary>
        /// <returns>The <see cref="IModalData"/>.</returns>
        IModalData CreateModalData();
    }
}
