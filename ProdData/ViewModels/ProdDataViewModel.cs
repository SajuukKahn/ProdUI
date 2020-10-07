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
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _playbackService.
        /// </summary>
        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdDataViewModel"/> class.
        /// </summary>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public ProdDataViewModel(IProgramDataService programDataService, IPlaybackService playbackService)
        {
            _programDataService = programDataService;
            _playbackService = playbackService;
            PlayButton = new DelegateCommand(() => PlaybackService.Play()).ObservesCanExecute(() => PlaybackService.PlayAvailable);
            PauseButton = new DelegateCommand(() => PlaybackService.Pause()).ObservesCanExecute(() => PlaybackService.PauseAvailable);
            OpenProgramSelect = new DelegateCommand(() => { ProgramDataService.ProgramRequestShow = true; }).ObservesCanExecute(() => ProgramDataService.AllowProgramChange);
        }

        /// <summary>
        /// Gets the ProgramDataService.
        /// </summary>
        public IProgramDataService ProgramDataService
        {
            get
            {
                return _programDataService;
            }
        }

        /// <summary>
        /// Gets the PlaybackService.
        /// </summary>
        public IPlaybackService PlaybackService
        {
            get
            {
                return _playbackService;
            }
        }

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
