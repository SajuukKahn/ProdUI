namespace ProductionCore.Interfaces
{
    using Prism.Commands;

    /// <summary>
    /// Defines the <see cref="ITestGeneratorViewModel" />.
    /// </summary>
    public interface ITestGeneratorViewModel
    {
        /// <summary>
        /// Gets the PlaybackService.
        /// </summary>
        public IPlaybackService PlaybackService { get; }

        /// <summary>
        /// Gets or sets the ChangeProcessImage.
        /// </summary>
        public DelegateCommand ChangeProcessImage { get; set; }

        /// <summary>
        /// Gets or sets the PauseButton.
        /// </summary>
        public DelegateCommand PauseButton { get; set; }

        /// <summary>
        /// Gets or sets the StartButton.
        /// </summary>
        public DelegateCommand StartButton { get; set; }

        /// <summary>
        /// Gets or sets the ThrowCardError.
        /// </summary>
        public DelegateCommand ThrowCardError { get; set; }
    }
}
