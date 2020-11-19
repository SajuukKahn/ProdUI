namespace ProductionCore.Interfaces
{
    using Prism.Commands;

    /// <summary>
    /// Defines the <see cref="IProgramSelectViewModel" />.
    /// </summary>
    public interface IProgramSelectViewModel
    {
        /// <summary>
        /// Gets the ProgramDataService.
        /// </summary>
        public IProgramDataService ProgramDataService { get; }

        /// <summary>
        /// Gets the MediationService.
        /// </summary>
        public IMediationService MediationService { get; }

        /// <summary>
        /// Gets or sets the ConfirmCommand.
        /// </summary>
        public DelegateCommand ConfirmCommand { get; set; }

        /// <summary>
        /// Gets or sets the CancelCommand.
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }
    }
}
