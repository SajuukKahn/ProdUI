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
        /// Gets or sets the ConfirmButton.
        /// </summary>
        public DelegateCommand ConfirmButton { get; set; }

        /// <summary>
        /// Gets or sets the CancelButton.
        /// </summary>
        public DelegateCommand CancelButton { get; set; }
    }
}
