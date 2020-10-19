namespace ProdData.Services
{
    using System.Collections.ObjectModel;
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="PlaybackService" />.
    /// </summary>
    public class PlaybackService : BindableBase, IPlaybackService
    {
        /// <summary>
        /// Defines the _modalService.
        /// </summary>
        private readonly IModalService _modalService;

        /// <summary>
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

        /// <summary>
        /// Defines the _programSteps.
        /// </summary>
        private ObservableCollection<ICard?> _programSteps = new ObservableCollection<ICard?>();

        /// <summary>
        /// Defines the _allowProgramChange.
        /// </summary>
        private bool _allowProgramChange = true;

        /// <summary>
        /// Defines the _programPaused.
        /// </summary>
        private bool _programPaused;

        /// <summary>
        /// Defines the _pauseAvailable.
        /// </summary>
        private bool _pauseAvailable;

        /// <summary>
        /// Defines the _playAvailable.
        /// </summary>
        private bool _playAvailable;

        /// <summary>
        /// Defines the _playbackRunning.
        /// </summary>
        private bool _playbackRunning;

        /// <summary>
        /// Defines the _currentCard.
        /// </summary>
        private ICard? _currentCard;

        /// <summary>
        /// Defines the _currentCardIndex.
        /// </summary>
        private int _currentCardIndex;

        /// <summary>
        /// Defines the _cycleTime.
        /// </summary>
        private IChronometer? _cycleTime;

        /// <summary>
        /// Defines the _productImage.
        /// </summary>
        private BitmapImage? _productImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackService"/> class.
        /// </summary>
        /// <param name="chronometerFactory">The chronometerFactory<see cref="IChronometerFactory"/>.</param>
        /// <param name="modalService">The modalService<see cref="IModalService"/>.</param>
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        public PlaybackService(IChronometerFactory chronometerFactory, IModalService modalService, IMediationService mediationService)
        {
            _cycleTime = chronometerFactory.Create();
            _modalService = modalService;
            _mediationService = mediationService;
        }

        /// <summary>
        /// Gets or sets the ProgramSteps.
        /// </summary>
        public ObservableCollection<ICard?> ProgramSteps
        {
            get
            {
                return _programSteps;
            }

            set
            {
                SetProperty(ref _programSteps, value, HandleChangedProgramData);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether AllowProgramChange.
        /// </summary>
        public bool AllowProgramChange
        {
            get
            {
                return _allowProgramChange;
            }

            set
            {
                SetProperty(ref _allowProgramChange, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramPaused.
        /// </summary>
        public bool ProgramPaused
        {
            get
            {
                return _programPaused;
            }

            set
            {
                SetProperty(ref _programPaused, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PauseAvailable.
        /// </summary>
        public bool PauseAvailable
        {
            get
            {
                return _pauseAvailable;
            }

            set
            {
                SetProperty(ref _pauseAvailable, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PlayAvailable.
        /// </summary>
        public bool PlayAvailable
        {
            get
            {
                return _playAvailable;
            }

            set
            {
                SetProperty(ref _playAvailable, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether PlaybackRunning.
        /// </summary>
        public bool PlaybackRunning
        {
            get
            {
                return _playbackRunning;
            }

            set
            {
                SetProperty(ref _playbackRunning, value);
            }
        }

        /// <summary>
        /// Gets or sets the CurrentCard.
        /// </summary>
        public ICard? CurrentCard
        {
            get
            {
                return _currentCard;
            }

            set
            {
                SetProperty(ref _currentCard, value);
            }
        }

        /// <summary>
        /// Gets or sets the CurrentCardIndex.
        /// </summary>
        public int CurrentCardIndex
        {
            get
            {
                return _currentCardIndex;
            }

            set
            {
                SetProperty(ref _currentCardIndex, value, SetCurrentCard);
            }
        }

        /// <summary>
        /// Gets or sets the CycleTime.
        /// </summary>
        public IChronometer? CycleTime
        {
            get
            {
                return _cycleTime;
            }

            set
            {
                SetProperty(ref _cycleTime, value);
            }
        }

        /// <summary>
        /// Gets or sets the ProductImage.
        /// </summary>
        public BitmapImage? ProductImage
        {
            get
            {
                return _productImage;
            }

            set
            {
                SetProperty(ref _productImage, value);
            }
        }

        /// <summary>
        /// The Abort.
        /// </summary>
        public void Abort()
        {
            Halt();

            CycleTime!.Reset();
            foreach (ICard? card in _programSteps!)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;

            if (ProgramSteps != null && _mediationService.CurrentProgram != null && CurrentCard!.StepModalData?.IsError == false)
            {
                if (_mediationService.CurrentProgram.AutoStartPlayback == true)
                {
                    Play();
                }
            }
        }

        /// <summary>
        /// The AdvanceStep.
        /// </summary>
        public void AdvanceStep()
        {
            if (CurrentCard!.IterateSubStep())
            {
                if (CurrentCard!.StepModalData != null && CurrentCard!.StepModalData!.IsError == false)
                {
                    _modalService.ShowModal(CurrentCard!.StepModalData);
                }
            }
            else if (CurrentCardIndex < _programSteps?.Count - 1)
            {
                CurrentCardIndex++;
                PlayCard();
            }
            else
            {
                CompleteCycle();
            }
        }

        /// <summary>
        /// The PauseCard.
        /// </summary>
        public void PauseCard()
        {
            if (CurrentCard != null)
            {
                CurrentCard.StepStatus = "Paused";
                CurrentCard.CardTime.Pause();
            }
        }

        /// <summary>
        /// The RetryStep.
        /// </summary>
        public void RetryCard()
        {
            CurrentCard?.RetryCard();
        }

        /// <summary>
        /// The StartCard.
        /// </summary>
        public void PlayCard()
        {
            CurrentCard?.StartCard();
        }

        /// <summary>
        /// The ModalEvent.
        /// </summary>
        /// <param name="modalData">The modalData<see cref="IModalData"/>.</param>
        public void ModalEvent(IModalData modalData)
        {
            if (CurrentCard!.StepModalData != null)
            {
                _modalService.ShowModal(CurrentCard!.StepModalData);
            }
        }

        /// <summary>
        /// The Pause.
        /// </summary>
        public void Pause()
        {
            PauseCard();
            ProgramPaused = true;
            PauseAvailable = false;
            _mediationService.PlaybackPaused = true;
            PlaybackRunning = false;
            PlayAvailable = true;
            AllowProgramChange = true;
        }

        /// <summary>
        /// The Play.
        /// </summary>
        public void Play()
        {
            if (_mediationService.CurrentProgram == null)
            {
                return;
            }

            if (PlaybackRunning == false)
            {
                CycleTime?.Start();
                _mediationService.BeginExecute = true;
            }

            CurrentCard!.StartCard();
            AllowProgramChange = false;
            PlaybackRunning = true;
            PlayAvailable = false;
            PauseAvailable = true;
            ProgramPaused = false;

            if (CurrentCard!.StepModalData != null && CurrentCard!.StepModalData.IsError == false)
            {
                _modalService.ShowModal(CurrentCard!.StepModalData);
            }
        }

        /// <summary>
        /// The RequestProgramChange.
        /// </summary>
        public void RequestProgramChange()
        {
            _mediationService.ProgramRequestShow = true;
        }

        /// <summary>
        /// The RaiseError.
        /// </summary>
        public void RaiseError()
        {
            Halt();
            if (CurrentCard!.StepModalData == null)
            {
                IModalData modalData = _modalService.CreateModalData();
                modalData.CanAbort = true;
                modalData.IsError = true;
                modalData.Card = CurrentCard;
                _modalService.ShowModal(modalData);
            }
            else
            {
                _modalService.ShowModal(CurrentCard!.StepModalData);
            }
        }

        /// <summary>
        /// The CompleteCycle.
        /// </summary>
        private void CompleteCycle()
        {
            Halt();
            _mediationService.SaveProgram(true, CycleTime);

            CycleTime!.Reset();
            foreach (ICard? card in _programSteps!)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;

            if (_programSteps != null && CurrentCard!.StepModalData?.IsError == false)
            {
                Play();
            }
        }

        /// <summary>
        /// The SetCurrentCard.
        /// </summary>
        private void SetCurrentCard()
        {
            if (_programSteps != null)
            {
                CurrentCard = _programSteps[CurrentCardIndex];
            }
        }

        /// <summary>
        /// The HandleChangedProgramData.
        /// </summary>
        private void HandleChangedProgramData()
        {
            // TODO: This doesn't fire when the collection is updated?
            CurrentCardIndex = 0;
            CurrentCard = _programSteps![0];
            PlayAvailable = _mediationService.CurrentProgram?.UserCanStartPlayback ?? false;
            CurrentCard!.IsActiveStep = true;
            ProductImage = _mediationService.CurrentProgram!.ProductImage;
            if (_mediationService.CurrentProgram.AutoStartPlayback == true)
            {
                Play();
            }
        }

        /// <summary>
        /// The Halt.
        /// </summary>
        private void Halt()
        {
            Pause();
            CycleTime?.Pause();
            _mediationService.EndExecute = true;
        }
    }
}
