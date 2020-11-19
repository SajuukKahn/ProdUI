namespace ProdCore.Interfaces
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
        /// Gets or sets the ChangeProcessImageCommand.
        /// </summary>
        public DelegateCommand ChangeProcessImageCommand { get; set; }

        /// <summary>
        /// Gets or sets the PauseCommand.
        /// </summary>
        public DelegateCommand PauseCommand { get; set; }

        /// <summary>
        /// Gets or sets the StartCommand.
        /// </summary>
        public DelegateCommand StartCommand { get; set; }

        /// <summary>
        /// Gets or sets the ThrowCardErrorCommand.
        /// </summary>
        public DelegateCommand ThrowCardErrorCommand { get; set; }
    }
}
