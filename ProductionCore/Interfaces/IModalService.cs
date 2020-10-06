namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IModalService" />.
    /// </summary>
    public interface IModalService
    {
        /// <summary>
        /// Gets or sets the ModalData.
        /// </summary>
        IModalData ModalData { get; set; }

        /// <summary>
        /// Gets or sets the ModalResponseData.
        /// </summary>
        IModalResponseData ModalResponseData { get; set; }

        /// <summary>
        /// The ShowModal.
        /// </summary>
        void ShowModal();

        /// <summary>
        /// The SendResponse.
        /// </summary>
        /// <param name="responseData">The responseData<see cref="IModalResponseData"/>.</param>
        /// <returns>The <see cref="IModalResponseData"/>.</returns>
        IModalResponseData SendResponse(IModalResponseData responseData);
    }
}
