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
        public DelegateCommand Abort { get; set; }

        /// <summary>
        /// Gets or sets the Continue.
        /// </summary>
        public DelegateCommand Continue { get; set; }

        /// <summary>
        /// Gets or sets the Custom.
        /// </summary>
        public DelegateCommand Custom { get; set; }

        /// <summary>
        /// Gets or sets the Retry.
        /// </summary>
        public DelegateCommand Retry { get; set; }
    }
}
