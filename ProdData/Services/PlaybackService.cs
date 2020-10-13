namespace ProdData.Services
{
    using System;
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
        /// Defines the _cardFactory.
        /// </summary>
        private readonly ICardFactory _cardFactory;

        /// <summary>
        /// Defines the _cardSubStepFactory.
        /// </summary>
        private readonly ICardSubStepFactory _cardSubStepFactory;

        /// <summary>
        /// Defines the _programSteps.
        /// </summary>
        private ObservableCollection<ICard?>? _programSteps;

        /// <summary>
        /// Defines the _programDataService.
        /// </summary>
        private IProgramDataService _programDataService;

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
        /// Defines the _runningProgram.
        /// </summary>
        private IProgramData? _runningProgram;

        /// <summary>
        /// Defines the _productImage.
        /// </summary>
        private BitmapImage? _productImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaybackService"/> class.
        /// </summary>
        /// <param name="chronometerFactory">The chronometerFactory<see cref="IChronometerFactory"/>.</param>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        /// <param name="modalService">The modalService<see cref="IModalService"/>.</param>
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        /// <param name="cardFactory">The cardFactory<see cref="ICardFactory"/>.</param>
        /// <param name="cardSubStepFactory">The cardSubStepFactory<see cref="ICardSubStepFactory"/>.</param>
        public PlaybackService(
            IChronometerFactory chronometerFactory,
            IProgramDataService programDataService,
            IModalService modalService,
            IMediationService mediationService,
            ICardFactory cardFactory,
            ICardSubStepFactory cardSubStepFactory)
        {
            _cycleTime = chronometerFactory.Create();
            _programDataService = programDataService;
            _modalService = modalService;
            _mediationService = mediationService;
            _cardFactory = cardFactory;
            _cardSubStepFactory = cardSubStepFactory;
        }

        /// <summary>
        /// Gets or sets the ProgramDataService.
        /// </summary>
        public IProgramDataService ProgramDataService
        {
            get
            {
                return _programDataService;
            }

            set
            {
                SetProperty(ref _programDataService, value, UpdateDataFromProgramDataService);
            }
        }

        /// <summary>
        /// The UpdateDataFromProgramDataService.
        /// </summary>
        private void UpdateDataFromProgramDataService()
        {
            RunningProgram = ProgramDataService.CurrentProgram;
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
        /// Gets or sets the RunningProgram.
        /// </summary>
        public IProgramData? RunningProgram
        {
            get
            {
                return _runningProgram;
            }

            set
            {
                SetProperty(ref _runningProgram, value);
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
            _programDataService.SaveProgram(null);

            CycleTime!.Reset();
            foreach (ICard? card in _programSteps!)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;

            if (ProgramSteps != null && RunningProgram != null && CurrentCard!.StepModalData?.IsError == false)
            {
                if (RunningProgram.AutoStartPlayback == true)
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
            if (RunningProgram == null)
            {
                return;
            }

            if (PlaybackRunning == false)
            {
                CycleTime?.Start();
                _mediationService.BeginExecution();
            }

            CurrentCard!.StartCard();
            AllowProgramChange = false;
            PlaybackRunning = true;
            PlayAvailable = false;
            PauseAvailable = true;
            ProgramPaused = false;
        }

        /// <summary>
        /// The RequestProgramChange.
        /// </summary>
        public void RequestProgramChange()
        {
            _programDataService.ProgramRequestShow = true;
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
        /// The LoadProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void LoadProgram(IProgramData? program = null)
        {
            if (program == null && RunningProgram == null)
            {
                return;
            }
            else if (program == null)
            {
                program = RunningProgram ?? null;
            }

            RetrieveProgram(program!);
        }

        /// <summary>
        /// The RetrieveProgram.
        /// </summary>
        /// <param name="programData">The programData <see cref="IProgramData"/>.</param>
        public void RetrieveProgram(IProgramData programData)
        {
            GenerateRandomProgram(programData);
        }

        /// <summary>
        /// The CompleteCycle.
        /// </summary>
        private void CompleteCycle()
        {
            Halt();
            _programDataService.IterateProgramCycles(null);
            _programDataService.UpdateProgramAverageCycleTime(null, CycleTime!.TimeSpan);
            _programDataService.SaveProgram(null);

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
            CurrentCardIndex = 0;
            CurrentCard = _programSteps![0];
            RunningProgram = _programDataService.CurrentProgram!;
            PlayAvailable = RunningProgram?.UserCanStartPlayback ?? false;
            CurrentCard!.IsActiveStep = true;
            ProductImage = RunningProgram?.ProductImage;
            if (RunningProgram?.AutoStartPlayback == true)
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
            _mediationService.EndExecution();
        }

        /// <summary>
        /// The GenerateRandomProgram.
        /// </summary>
        /// <param name="programData">The programData <see cref="IProgramData"/>.</param>
        private void GenerateRandomProgram(IProgramData programData)
        {
            string[] titleArray = { "PolyLine 3D", "Area", "Move", "Line", "PolyLine", "Arc", "Spiral", "Rectangular Sprial", "Dot", "Part Presense Check" };

            BitmapImage? image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\FID.bmp", UriKind.RelativeOrAbsolute));

            ProgramSteps?.Clear();

            if (programData?.ProgramName == "Manual Placement Simulation")
            {
                ICard manualCard = _cardFactory.Create();
                manualCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Place Product", new string[] { string.Empty }));
                manualCard.StepTitle = "Place Product";
                manualCard.StepImage = programData.ProductImage;
                manualCard.StepModalData = _modalService.CreateModalData();
                manualCard.StepModalData!.CanAbort = true;
                manualCard.StepModalData!.CanContinue = true;
                manualCard.StepModalData!.CanRetry = false;
                manualCard.StepModalData!.Card = null;
                manualCard.StepModalData!.Instructions = "Place Product and press 'Continue' to begin" + Environment.NewLine + "Press 'Abort' to exit path playback";
                manualCard.StepModalData!.InstructionImage = programData.ProductImage;
                manualCard.StepModalData!.IsError = false;
                ProgramSteps?.Add(manualCard);
            }

            ICard fiducialCard = _cardFactory.Create();
            fiducialCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial A", RandomCoordinates()));
            fiducialCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Fiducial B", RandomCoordinates()));
            fiducialCard.StepTitle = "Fiducial Check";
            fiducialCard.StepImage = image;
            fiducialCard.StepModalData = _modalService.CreateModalData();
            fiducialCard.StepModalData!.CanAbort = true;
            fiducialCard.StepModalData!.CanContinue = true;
            fiducialCard.StepModalData!.CanRetry = true;
            fiducialCard.StepModalData!.Card = null;
            fiducialCard.StepModalData!.Instructions = "Fiducials Failed, Select an option below";
            fiducialCard.StepModalData!.InstructionImage = image;
            fiducialCard.StepModalData!.IsError = false;
            ProgramSteps?.Add(fiducialCard);
            int randSize = new Random().Next(2, 8);

            ICard surfaceCard = _cardFactory.Create();
            for (int i = 0; i < randSize; i++)
            {
                surfaceCard.CardSubSteps?.Add(_cardSubStepFactory.Create("Surface " + (i + 1), RandomCoordinates()));
            }

            surfaceCard.StepTitle = "Surface Height Check";
            surfaceCard.StepModalData = _modalService.CreateModalData();
            surfaceCard.StepModalData!.CanAbort = true;
            surfaceCard.StepModalData!.CanRetry = true;
            surfaceCard.StepModalData!.Card = null;
            surfaceCard.StepModalData!.Instructions = "Surface Height Checks failed, Select an option below";
            surfaceCard.StepModalData!.IsError = false;
            ProgramSteps?.Add(surfaceCard);

            randSize = new Random().Next(4, 12);
            for (int i = 0; i < randSize; i++)
            {
                int randSteps = new Random().Next(1, 8);
                string stepTitle = titleArray[new Random().Next(titleArray.Length)];
                ICard card = _cardFactory.Create();
                card.CardSubSteps?.Add(_cardSubStepFactory.Create(stepTitle, RandomCoordinates()));
                card.StepTitle = stepTitle;
                for (int j = 0; j < randSteps; j++)
                {
                    card.CardSubSteps?.Add(_cardSubStepFactory.Create(stepTitle + " " + (j + 1), RandomCoordinates()));
                }

                if (new Random().Next(0, 4) == 1)
                {
                    image = new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
                    card.StepImage = image;
                }

                if (stepTitle == "Part Presence Check")
                {
                    card.StepModalData = _modalService.CreateModalData();
                    card.StepModalData!.CanAbort = true;
                    card.StepModalData!.CanContinue = true;
                    card.StepModalData!.CanRetry = true;
                    card.StepModalData!.Card = null;
                    card.StepModalData!.Instructions = "Part Presence Check failed, Select an option below";
                    card.StepModalData!.IsError = false;
                }

                ProgramSteps?.Add(card);
            }
        }

        /// <summary>
        /// The ChooseRandomImage.
        /// </summary>
        /// <returns>The <see cref="BitmapImage"/>.</returns>
        private BitmapImage ChooseRandomImage()
        {
            return new BitmapImage(new Uri(Environment.CurrentDirectory + "\\Modules\\TestImages\\PCB" + new Random().Next(1, 9).ToString() + ".bmp", UriKind.RelativeOrAbsolute));
        }

        /// <summary>
        /// The RandomCoordinates.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        private string[] RandomCoordinates()
        {
            return new string[] { new Random().Next(-20000, 200000).ToString(), new Random().Next(-20000, 200000).ToString(), new Random().Next(-9000, 100000).ToString() };
        }
    }
}
