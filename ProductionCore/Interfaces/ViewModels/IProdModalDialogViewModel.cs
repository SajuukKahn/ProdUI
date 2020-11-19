namespace ProductionCore.Interfaces
{
    using Prism.Commands;

    /// <summary>
    /// Defines the <see cref="IProdModalDialogViewModel" />.
    /// </summary>
    public interface IProdModalDialogViewModel
    {
        /// <summary>
        /// Gets the ModalService.
        /// </summary>
        public IModalService ModalService { get; }

        /// <summary>
        /// Gets or sets the Abort.
        /// </summary>
        public DelegateCommand AbortCommand { get; set; }

        /// <summary>
        /// Gets or sets the Continue.
        /// </summary>
        public DelegateCommand ContinueCommand { get; set; }

        /// <summary>
        /// Gets or sets the Custom.
        /// </summary>
        public DelegateCommand CustomCommand { get; set; }

        /// <summary>
        /// Gets or sets the Retry.
        /// </summary>
        public DelegateCommand RetryCommand { get; set; }
    }
}
