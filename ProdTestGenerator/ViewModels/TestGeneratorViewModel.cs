namespace ProdTestGenerator.ViewModels
{
    using System;
    using System.Windows.Media.Imaging;
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
    public class TestGeneratorViewModel : BindableBase, ITestGeneratorViewModel
    {
        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestGeneratorViewModel"/> class.
        /// </summary>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public TestGeneratorViewModel(IPlaybackService playbackService)
        {
            _playbackService = playbackService;
            StartButton = new DelegateCommand(() => PlaybackService.Play(), PlaybackButtonCanExecute);
            PauseButton = new DelegateCommand(() => PlaybackService.Pause()).ObservesCanExecute(() => PlaybackService.PauseAvailable);
            ChangeProcessImage = new DelegateCommand(() => { PlaybackService.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute)); });
            ThrowCardError = new DelegateCommand(() => PlaybackService.RaiseError()).ObservesCanExecute(() => PlaybackService.PlaybackRunning);
        }

        /// <inheritdoc/>
        public IPlaybackService PlaybackService
        {
            get
            {
                return _playbackService;
            }
        }

        /// <inheritdoc/>
        public DelegateCommand ChangeProcessImage { get; set; }

        /// <inheritdoc/>
        public DelegateCommand PauseButton { get; set; }

        /// <inheritdoc/>
        public DelegateCommand StartButton { get; set; }

        /// <inheritdoc/>
        public DelegateCommand ThrowCardError { get; set; }

        /// <summary>
        /// The PlaybackButtonCanExecute.
        /// </summary>
        /// <returns>True if Not Playback.PauseAvailable.</returns>
        private bool PlaybackButtonCanExecute()
        {
            return !PlaybackService.PauseAvailable;
        }
    }
}
