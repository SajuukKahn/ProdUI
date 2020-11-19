namespace ProdData.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProdDataViewModel" />.
    /// </summary>
    public class ProdDataViewModel : BindableBase, IProdDataViewModel
    {
        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdDataViewModel"/> class.
        /// </summary>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        public ProdDataViewModel(IPlaybackService playbackService, IMediationService mediationService)
        {
            _playbackService = playbackService;
            _mediationService = mediationService;
            PlayCommand = new DelegateCommand(Play).ObservesCanExecute(() => PlaybackService.PlayAvailable);
            PauseCommand = new DelegateCommand(Pause).ObservesCanExecute(() => PlaybackService.PauseAvailable);
            ProgramSelectCommand = new DelegateCommand(ProgramSelect).ObservesCanExecute(() => PlaybackService.AllowProgramChange);
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
        public IMediationService MediationService
        {
            get
            {
                return _mediationService;
            }
        }

        /// <inheritdoc/>
        public DelegateCommand ProgramSelectCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand PauseCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand PlayCommand { get; set; }

        /// <summary>
        /// The ProgramSelectCommand Method call.
        /// </summary>
        private void ProgramSelect()
        {
            PlaybackService.RequestProgramChange();
        }

        /// <summary>
        /// The PauseCommand Method call.
        /// </summary>
        private void Pause()
        {
            PlaybackService.Pause();
        }

        /// <summary>
        /// The PlayCommand Method call.
        /// </summary>
        private void Play()
        {
            PlaybackService.Play();
        }
    }
}
