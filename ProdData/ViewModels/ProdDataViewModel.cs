﻿namespace ProdData.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows.Media.Imaging;
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using ProductionCore.Concrete;
    using ProductionCore.Events;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProdDataViewModel" />.
    /// </summary>
    public class ProdDataViewModel : BindableBase, IProdDataViewModel
    {
        /// <summary>
        /// Defines the _eventAggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Defines the _allowProgramChange.
        /// </summary>
        private bool _allowProgramChange = true;

        /// <summary>
        /// Defines the _cardCollection.
        /// </summary>
        private ObservableCollection<Card?> _cardCollection = new ObservableCollection<Card?>();

        /// <summary>
        /// Defines the _currentCard.
        /// </summary>
        private Card? _currentCard;

        /// <summary>
        /// Defines the _currentCardIndex.
        /// </summary>
        private int _currentCardIndex;

        /// <summary>
        /// Defines the _cycleCount.
        /// </summary>
        private long _cycleCount;

        /// <summary>
        /// Defines the _cycleTime.
        /// </summary>
        private Timer _cycleTime = new Timer();

        /// <summary>
        /// Defines the _pauseAvailable.
        /// </summary>
        private bool _pauseAvailable;

        /// <summary>
        /// Defines the _playAvailable.
        /// </summary>
        private bool _playAvailable;

        /// <summary>
        /// Defines the _playBackRunning.
        /// </summary>
        private bool _playBackRunning;

        /// <summary>
        /// Defines the _productImage.
        /// </summary>
        private BitmapImage? _productImage;

        /// <summary>
        /// Defines the _selectedProgramData.
        /// </summary>
        private IProgramData? _selectedProgramData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdDataViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="programDataFactory">The programDataFactory<see cref="IProgramDataFactory"/>.</param>
        public ProdDataViewModel(IEventAggregator eventAggregator, IProgramDataFactory programDataFactory)
        {
            _eventAggregator = eventAggregator;
            PlayButton = new DelegateCommand(PlayPressed).ObservesCanExecute(() => PlayAvailable);
            PauseButton = new DelegateCommand(PausePressed).ObservesCanExecute(() => PauseAvailable);
            OpenProgramSelect = new DelegateCommand(RequestProgramSelect).ObservesCanExecute(() => AllowProgramChange);
            _eventAggregator.GetEvent<ProgramDataResponse>().Subscribe(HandleProgramDataResponse);
            _eventAggregator.GetEvent<StartRequest>().Subscribe(HandleStartRequest);
            _eventAggregator.GetEvent<ProgramPaused>().Subscribe(HandlePauseConfirmation);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(HandlePauseRequest);
            _eventAggregator.GetEvent<ProgramDataSaveResponse>().Subscribe(HandleProgramDataSaveResponse);
            _eventAggregator.GetEvent<ProgramSelectResponse>().Subscribe(HandleProgramSelectResponse);
            _eventAggregator.GetEvent<ProductImageChangeResponse>().Subscribe(HandleProductImageChangeResponse);
            _eventAggregator.GetEvent<RaiseError>().Subscribe(HandleRaiseError);
            _eventAggregator.GetEvent<AdvanceStep>().Subscribe(HandleAdvanceStep);
            _eventAggregator.GetEvent<ProgramHaltRequest>().Subscribe(HandleProgramHaltRequest);
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalAbortRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Abort));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalContinueRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Continue));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalRetryRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Retry));
            _eventAggregator.GetEvent<ModalResponse>().Subscribe(HandleModalCustomRequest, ThreadOption.BackgroundThread, false, modalData => modalData.Equals(ModalResponseData.Custom));
            _selectedProgramData = programDataFactory.Create();

            AllowProgramChange = true;
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
        /// Gets or sets the CardCollection.
        /// </summary>
        public ObservableCollection<Card?> CardCollection
        {
            get
            {
                return _cardCollection;
            }

            set
            {
                SetProperty(ref _cardCollection, value);
            }
        }

        /// <summary>
        /// Gets or sets the CurrentCard.
        /// </summary>
        public Card? CurrentCard
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
                Debug.WriteLine(nameof(value) + " Set to: " + value.ToString());
                SetProperty(ref _currentCardIndex, value, SetCurrentCard);
            }
        }

        /// <summary>
        /// Gets or sets the CycleCount.
        /// </summary>
        public long CycleCount
        {
            get
            {
                return _cycleCount;
            }

            set
            {
                SetProperty(ref _cycleCount, value);
            }
        }

        /// <summary>
        /// Gets or sets the CycleTime.
        /// </summary>
        public Timer CycleTime
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
        /// Gets or sets the OpenProgramSelect.
        /// </summary>
        public DelegateCommand OpenProgramSelect { get; set; }

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
        /// Gets or sets the PauseButton.
        /// </summary>
        public DelegateCommand PauseButton { get; set; }

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
        /// Gets or sets a value indicating whether PlayBackRunning.
        /// </summary>
        public bool PlayBackRunning
        {
            get
            {
                return _playBackRunning;
            }

            set
            {
                SetProperty(ref _playBackRunning, value);
            }
        }

        /// <summary>
        /// Gets or sets the PlayButton.
        /// </summary>
        public DelegateCommand PlayButton { get; set; }

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
        /// Gets or sets the SelectedProgramData.
        /// </summary>
        public IProgramData? SelectedProgramData
        {
            get
            {
                return _selectedProgramData;
            }

            set
            {
                SetProperty(ref _selectedProgramData, value, UpdateProductImage);
            }
        }

        // TODO I don't think this is a good way to handle going to the next card
        // It should probably be a method contained in the target object?

        /// <summary>
        /// The HandleAdvanceStep.
        /// </summary>
        public void HandleAdvanceStep()
        {
            if (IterateSubStep())
            {
            }
            else if (CurrentCardIndex < _cardCollection?.Count - 1)
            {
                CurrentCardIndex++;
                StartCard();
            }
            else
            {
                CompleteCycle();
            }
        }

        /// <summary>
        /// The IterateSubStep.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool IterateSubStep()
        {
            if (_cardCollection[CurrentCardIndex]?.CardStepIndex < _cardCollection[CurrentCardIndex]?.CardSubSteps.Count - 1)
            {
                _cardCollection[CurrentCardIndex] !.CardStepIndex++;
                return true;
            }

            _cardCollection[CurrentCardIndex] !.StepStatus = StepStatus.Completed;
            _cardCollection[CurrentCardIndex] !.StepComplete = true;
            _cardCollection[CurrentCardIndex] !.IsActiveStep = false;
            _cardCollection[CurrentCardIndex] !.CardTime.Pause();
            return false;
        }

        /// <summary>
        /// The PauseCard.
        /// </summary>
        public void PauseCard()
        {
            _cardCollection[CurrentCardIndex] !.StepStatus = StepStatus.Paused;
            _cardCollection[CurrentCardIndex] !.CardTime.Pause();
        }

        /// <summary>
        /// The RetryStep.
        /// </summary>
        public void RetryStep()
        {
            CurrentCard?.RetryCard();
            //// _cardCollection[CurrentCardIndex]!.CardStepIndex = 0;
            //// _cardCollection[CurrentCardIndex]!.CardTime.Reset();
            //// _cardCollection[CurrentCardIndex]!.CardTime.Start();
        }

        /// <summary>
        /// The StartCard.
        /// </summary>
        public void StartCard()
        {
            CurrentCard?.StartCard();
            //// _cardCollection[CurrentCardIndex]!.IsActiveStep = true;
            //// _cardCollection[CurrentCardIndex]!.StepStatus = StepStatus.Running;
            //// _cardCollection[CurrentCardIndex]!.CardTime.Start();
            //// if (_cardCollection[CurrentCardIndex]!.StepModalData?.IsError == false)
            //// {
            ////    _eventAggregator.GetEvent<ModalEvent>().Publish(_cardCollection[CurrentCardIndex]!.StepModalData!);
            //// }
        }

        /// <summary>
        /// The CompleteCycle.
        /// </summary>
        private void CompleteCycle()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            SelectedProgramData!.UpdateAverageCycleTime(CycleTime.TimeSpan);
            CycleCount++;
            SelectedProgramData!.HistoricalCycles = CycleCount;
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Publish();
            if (_cardCollection[CurrentCardIndex] !.StepModalData?.IsError == false)
            {
                _eventAggregator.GetEvent<StartRequest>().Publish();
            }
        }

        /// <summary>
        /// The HandleModalAbortRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalAbortRequest(ModalResponseData obj)
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Publish();
        }

        /// <summary>
        /// The HandleModalContinueRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalContinueRequest(ModalResponseData obj)
        {
            // PlaybackStart();
            IterateSubStep();
        }

        /// <summary>
        /// The HandleModalCustomRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalCustomRequest(ModalResponseData obj)
        {
        }

        /// <summary>
        /// The HandleModalRetryRequest.
        /// </summary>
        /// <param name="obj">The obj<see cref="ModalResponseData"/>.</param>
        private void HandleModalRetryRequest(ModalResponseData obj)
        {
            PlaybackStart();
            RetryStep();
        }

        /// <summary>
        /// The HandlePauseConfirmation.
        /// </summary>
        private void HandlePauseConfirmation()
        {
            PauseCard();
        }

        /// <summary>
        /// The HandlePauseRequest.
        /// </summary>
        private void HandlePauseRequest()
        {
            PlayBackRunning = false;
            PlayAvailable = true;
            PauseAvailable = false;
            AllowProgramChange = true;
        }

        /// <summary>
        /// The HandleProductImageChangeResponse.
        /// </summary>
        /// <param name="image">The image<see cref="BitmapImage"/>.</param>
        private void HandleProductImageChangeResponse(BitmapImage? image)
        {
            ProductImage = image;
        }

        /// <summary>
        /// The HandleProgramDataResponse.
        /// </summary>
        /// <param name="publishedCardCollection">The publishedCardCollection<see cref="ObservableCollection{Card}"/>.</param>
        private void HandleProgramDataResponse(ObservableCollection<Card?> publishedCardCollection)
        {
            CardCollection = publishedCardCollection;
            CurrentCardIndex = 0;
            CycleCount = SelectedProgramData!.HistoricalCycles;
            PlayAvailable = SelectedProgramData.UserCanStartPlayback;
            _cardCollection[CurrentCardIndex] !.IsActiveStep = true;
            if (SelectedProgramData.AutoStartPlayback)
            {
                _eventAggregator.GetEvent<StartRequest>().Publish();
            }
        }

        /// <summary>
        /// The HandleProgramDataSaveResponse.
        /// </summary>
        private void HandleProgramDataSaveResponse()
        {
            CycleTime.Reset();

            foreach (Card? card in _cardCollection)
            {
                card!.Initialize();
            }

            CurrentCardIndex = 0;
        }

        /// <summary>
        /// The HandleProgramHaltRequest.
        /// </summary>
        private void HandleProgramHaltRequest()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
        }

        /// <summary>
        /// The HandleProgramSelectResponse.
        /// </summary>
        /// <param name="programData">The programData<see cref="IProgramData"/>.</param>
        private void HandleProgramSelectResponse(IProgramData? programData)
        {
            SelectedProgramData = programData!;
        }

        /// <summary>
        /// The HandleRaiseError.
        /// </summary>
        private void HandleRaiseError()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            if (_cardCollection[CurrentCardIndex] !.StepModalData == null)
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(new ModalData()
                {
                    CanAbort = true,
                    IsError = true,
                    Card = CurrentCard,
                });
            }
            else
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(_cardCollection[CurrentCardIndex] !.StepModalData!);
            }
        }

        /// <summary>
        /// The HandleStartRequest.
        /// </summary>
        private void HandleStartRequest()
        {
            if (PlayBackRunning == false && SelectedProgramData != null)
            {
                PlaybackStart();
            }
        }

        /// <summary>
        /// The PausePressed.
        /// </summary>
        private void PausePressed()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        /// <summary>
        /// The PlaybackStart.
        /// </summary>
        private void PlaybackStart()
        {
            PlayAvailable = false;
            PauseAvailable = true;
            CycleTime.Start();
            StartCard();
            PlayBackRunning = true;
            AllowProgramChange = false;
        }

        /// <summary>
        /// The PlayPressed.
        /// </summary>
        private void PlayPressed()
        {
            _eventAggregator.GetEvent<StartRequest>().Publish();
        }

        /// <summary>
        /// The RequestProgramSelect.
        /// </summary>
        private void RequestProgramSelect()
        {
            _eventAggregator.GetEvent<ProgramSelectRequest>().Publish(SelectedProgramData);
        }

        /// <summary>
        /// The SetCurrentCard.
        /// </summary>
        private void SetCurrentCard()
        {
            CurrentCard = _cardCollection[CurrentCardIndex];
        }

        /// <summary>
        /// The UpdateProductImage.
        /// </summary>
        private void UpdateProductImage()
        {
            ProductImage = SelectedProgramData!.ProductImage;
        }
    }
}
