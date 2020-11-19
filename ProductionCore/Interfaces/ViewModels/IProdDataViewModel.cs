namespace ProductionCore.Interfaces
{
    using Prism.Commands;

    /// <summary>
    /// Defines the <see cref="IProdDataViewModel" />.
    /// </summary>
    public interface IProdDataViewModel
    {
        /// <summary>
        /// Gets the PlaybackService.
        /// </summary>
        public IPlaybackService PlaybackService { get; }

        /// <summary>
        /// Gets the MediationService.
        /// </summary>
        public IMediationService MediationService { get; }

        /// <summary>
        /// Gets or sets the ProgramSelectCommand.
        /// </summary>
        public DelegateCommand ProgramSelectCommand { get; set; }

        /// <summary>
        /// Gets or sets the PauseCommand.
        /// </summary>
        public DelegateCommand PauseCommand { get; set; }

        /// <summary>
        /// Gets or sets the PlayCommand.
        /// </summary>
        public DelegateCommand PlayCommand { get; set; }
    }
}
