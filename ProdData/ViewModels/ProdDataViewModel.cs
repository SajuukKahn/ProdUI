﻿namespace ProdData.ViewModels
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
            PlayButton = new DelegateCommand(() => PlaybackService.Play()).ObservesCanExecute(() => PlaybackService.PlayAvailable);
            PauseButton = new DelegateCommand(() => PlaybackService.Pause()).ObservesCanExecute(() => PlaybackService.PauseAvailable);
            OpenProgramSelect = new DelegateCommand(() => PlaybackService.RequestProgramChange()).ObservesCanExecute(() => PlaybackService.AllowProgramChange);
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
        /// Gets the MediationService.
        /// </summary>
        public IMediationService MediationService
        {
            get
            {
                return _mediationService;
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
