namespace ProdData.Services
{
    using System;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Windows.Media.Imaging;
    using Newtonsoft.Json;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
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

        /// <inheritdoc/>
        public event Action? PauseInitiated;

        /// <inheritdoc/>
        public event Action? PlaybackInitiated;

        /// <inheritdoc/>
        public event Action? AbortInitiated;

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public void Abort()
        {
            Pause();
            CycleTime!.Pause();
            PlaybackRunning = false;
            AbortInitiated!();

            CycleTime!.Reset();
            foreach (ICard? card in _programSteps!)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;
            CurrentCard = ProgramSteps[0];

            if (ProgramSteps != null && _mediationService.CurrentProgram != null && CurrentCard!.StepModalData?.IsError == false && _mediationService.CurrentProgram.AutoStartPlayback == true && _modalService.ModalActive == false)
            {
                Play();
            }
            else
            {
                PlayAvailable = true;
                AllowProgramChange = true;
            }
        }

        /// <inheritdoc/>
        public void AdvanceStep()
        {
            PlayAvailable = false;
            PauseAvailable = true;
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

        /// <inheritdoc/>
        public void PauseCard()
        {
            if (CurrentCard != null)
            {
                CurrentCard.StepStatus = "Paused";
                CurrentCard.CardTime.Pause();
            }
        }

        /// <inheritdoc/>
        public void RetryCard()
        {
            PlaybackInitiated!();
            PauseAvailable = true;
            PlayAvailable = false;
            CurrentCard?.RetryCard();
        }

        /// <inheritdoc/>
        public void PlayCard()
        {
            CurrentCard?.StartCard();
        }

        /// <inheritdoc/>
        public void ModalEvent(IModalData modalData)
        {
            if (CurrentCard!.StepModalData != null)
            {
                _modalService.ShowModal(CurrentCard!.StepModalData);
            }
        }

        /// <inheritdoc/>
        public void Pause()
        {
            if (CurrentCard == null)
            {
                return;
            }

            CurrentCard!.StepStatus = "Pausing...";
            ProgramPaused = true;
            PauseAvailable = false;
            PauseInitiated!();
        }

        /// <inheritdoc/>
        public void RunningStepPaused()
        {
            PauseCard();
            PlayAvailable = true;
            AllowProgramChange = true;
        }

        /// <inheritdoc/>
        public void Play()
        {
            if (_mediationService.CurrentProgram == null)
            {
                return;
            }

            if (PlaybackRunning == false)
            {
                CycleTime?.Start();
            }

            AllowProgramChange = false;
            PlaybackRunning = true;
            if (CurrentCard!.StepModalData != null && CurrentCard!.StepModalData!.IsError == false && _modalService.ModalActive == false)
            {
                _modalService.ShowModal(CurrentCard!.StepModalData);
                PlaybackInitiated!();
                ProgramPaused = true;
                PauseInitiated!();
                PlayAvailable = false;
                PauseAvailable = false;
            }
            else
            {
                PlaybackInitiated!();
                PlayAvailable = false;
                PauseAvailable = true;
                ProgramPaused = false;
            }

            AllowProgramChange = false;
            CurrentCard!.StartCard();
        }

        /// <inheritdoc/>
        public void RequestProgramChange()
        {
            _mediationService.ProgramRequestShow = true;
        }

        /// <inheritdoc/>
        public void RaiseError()
        {
            if (CurrentCard == null)
            {
                return;
            }

            Pause();

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
            Pause();
            PauseCard();
            PlaybackRunning = false;
            ProgramPaused = false;
            _mediationService.SaveProgram(true, CycleTime);
            SaveProgram();
            CycleTime!.Reset();
            foreach (ICard? card in _programSteps!)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;

            if (_programSteps != null && _mediationService!.CurrentProgram!.AutoStartPlayback && CurrentCard!.StepModalData?.IsError == false)
            {
                Play();
            }
            else
            {
                AllowProgramChange = true;
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
            PlaybackRunning = false;
            PauseAvailable = false;
            PlayAvailable = false;
            ProgramPaused = true;
            CycleTime!.Pause();
            CycleTime!.Reset();
            PlayAvailable = _mediationService.CurrentProgram?.UserCanStartPlayback ?? false;
            CurrentCard!.IsActiveStep = true;
            ProductImage = _mediationService.CurrentProgram!.ProductImage;
            if (_mediationService.CurrentProgram.AutoStartPlayback == true)
            {
                AllowProgramChange = false;
                PauseAvailable = true;
                Play();
            }
            else
            {
                AllowProgramChange = true;
                PlayAvailable = true;
            }
        }

        /// <summary>
        /// The SaveProgram.
        /// </summary>
        private void SaveProgram()
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
            string fileName = filePath + _mediationService.CurrentProgram!.ProgramName +
                               "__Run#" + _mediationService.CurrentProgram!.Cycles + "__" +
                               DateTime.Now.ToString("yyyy-MM-d--HH-mm-ss") + ".json";
            var serializedPathData = JsonConvert.SerializeObject(ProgramSteps, Formatting.Indented);
            using StreamWriter stream = new StreamWriter(fileName, false);
            stream.Write(serializedPathData);
        }
    }
}
