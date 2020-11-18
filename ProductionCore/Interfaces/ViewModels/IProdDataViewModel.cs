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
        /// Gets or sets the OpenProgramSelect.
        /// </summary>
        public DelegateCommand OpenProgramSelect { get; set; }

        /// <summary>
        /// Gets or sets the PauseButton.
        /// </summary>
        public DelegateCommand PauseButton { get; set; }

        /// <summary>
        /// Gets or sets the PlayButton.
        /// </summary>
        public DelegateCommand PlayButton { get; set; }
    }
}
