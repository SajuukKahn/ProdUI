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
            StartCommand = new DelegateCommand(Start, PlaybackButtonCanExecute);
            PauseCommand = new DelegateCommand(Pause).ObservesCanExecute(() => PlaybackService.PauseAvailable);
            ChangeProcessImageCommand = new DelegateCommand(ChangeProcessImage);
            ThrowCardErrorCommand = new DelegateCommand(ThrowCardError).ObservesCanExecute(() => PlaybackService.PlaybackRunning);
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
        public DelegateCommand ChangeProcessImageCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand PauseCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand StartCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand ThrowCardErrorCommand { get; set; }

        /// <summary>
        /// The PlaybackButtonCanExecute.
        /// </summary>
        /// <returns>True if Not Playback.PauseAvailable.</returns>
        private bool PlaybackButtonCanExecute()
        {
            return !PlaybackService.PauseAvailable;
        }

        /// <summary>
        /// The ThrowCardErrorCommand Method.
        /// </summary>
        private void ThrowCardError()
        {
            PlaybackService.RaiseError();
        }

        /// <summary>
        /// The ChangeProcessImageCommand Method.
        /// </summary>
        private void ChangeProcessImage()
        {
            PlaybackService.ProductImage = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// The PauseCommand Method.
        /// </summary>
        private void Pause()
        {
            PlaybackService.Pause();
        }

        /// <summary>
        /// The StartCommand Method.
        /// </summary>
        private void Start()
        {
            PlaybackService.Play();
        }

    }
}
