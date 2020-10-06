namespace ProdData.Services
{
    using Prism.Mvvm;
    using ProductionCore.Events;
    using ProductionCore.Interfaces;
    using System.Collections.ObjectModel;
    using System.Security.Cryptography.Pkcs;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="PlaybackService" />.
    /// </summary>
    public class PlaybackService : BindableBase, IPlaybackService
    {
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _programSteps.
        /// </summary>
        private ObservableCollection<ICard?>? _programSteps;

        /// <summary>
        /// Defines the _programPaused.
        /// </summary>
        private bool _programPaused;

        /// <summary>
        /// Defines the _allowProgramChange.
        /// </summary>
        private bool _allowProgramChange;

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
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        public PlaybackService(IChronometerFactory chronometerFactory, IProgramDataService programDataService)
        {
            _cycleTime = chronometerFactory.Create();
            _programDataService = programDataService;
        }

        /// <summary>
        /// Gets or sets the ProgramSteps.
        /// </summary>
        public ObservableCollection<ICard?>? ProgramSteps
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
        /// The AdvanceStep.
        /// </summary>
        public void AdvanceStep()
        {
            if (IterateSubStep())
            {
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
        /// The Halt.
        /// </summary>
        public void Halt()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The LoadProgramData.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void LoadProgramData(IProgramData program)
        {
        }

        /// <summary>
        /// The ModalEvent.
        /// </summary>
        /// <param name="modalData">The modalData<see cref="IModalData"/>.</param>
        public void ModalEvent(IModalData modalData)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The ModalResponse.
        /// </summary>
        /// <returns>The <see cref="IModalResponseData"/>.</returns>
        public IModalResponseData ModalResponse()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The Pause.
        /// </summary>
        public void Pause()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The Play.
        /// </summary>
        public void Play()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The RaiseError.
        /// </summary>
        public void RaiseError()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// The SaveProgramData.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void SaveProgramData(IProgramData program)
        {
            CycleTime!.Reset();

            foreach (ICard? card in _programSteps!)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;
        }

        /// <summary>
        /// The IterateSubStep.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        private bool IterateSubStep()
        {
            if (_programSteps != null && _programSteps[CurrentCardIndex] != null)
            {
                if (_programSteps[CurrentCardIndex]?.CardStepIndex < _programSteps[CurrentCardIndex]?.CardSubSteps?.Count - 1)
                {
                    _programSteps[CurrentCardIndex]!.CardStepIndex++;
                    return true;
                }

                _programSteps[CurrentCardIndex]!.StepStatus = "Completed";
                _programSteps[CurrentCardIndex]!.StepComplete = true;
                _programSteps[CurrentCardIndex]!.IsActiveStep = false;
                _programSteps[CurrentCardIndex]!.CardTime.Pause();
                return false;
            }

            return true;
        }

        /// <summary>
        /// The CompleteCycle.
        /// </summary>
        private void CompleteCycle()
        {
            Pause();
            CycleTime!.Pause();
            _programDataService.IterateProgramCycles(null);
            _programDataService.UpdateProgramAverageCycleTime(null, CycleTime.TimeSpan);
            _programDataService.SaveProgram(null);
            if (_programSteps != null && _programSteps[CurrentCardIndex]!.StepModalData?.IsError == false)
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
            CurrentCardIndex = 0;
            CurrentCard = _programSteps![0];
            PlayAvailable = _programDataService.SelectedProgramData?.UserCanStartPlayback ?? false;
            CurrentCard!.IsActiveStep = true;
            if (_programDataService.SelectedProgramData?.AutoStartPlayback == true)
            {
                Play();
            }
        }
    }
}
