using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProductionCore.Concrete;
using ProductionCore.Events;
using ProductionCore.Interfaces;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace ProdData.ViewModels
{
    public class ProdDataViewModel : BindableBase, IProdDataViewModel
    {
        private readonly IEventAggregator _eventAggregator;
        private bool _allowProgramChange = true;

        private ObservableCollection<Card?> _cardCollection = new ObservableCollection<Card?>();
        private Card? _currentCard;
        private int _currentCardIndex;
        private long _cycleCount;
        private Timer _cycleTime = new Timer();
        private bool _pauseAvailable;
        private bool _playAvailable;
        private bool _playBackRunning;
        private BitmapImage? _productImage;
        private IProgramData? _selectedProgramData;

        //TODO Is this ViewModel doing too much heavy lifting?  Is this whole class too complicated?

        public ProdDataViewModel(IEventAggregator eventAggregator, IProgramDataFactory programDataFactory)
        {
            _selectedProgramData = programDataFactory.Create();
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

            AllowProgramChange = true;
        }

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

        public DelegateCommand OpenProgramSelect { get; set; }

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

        public DelegateCommand PauseButton { get; set; }

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

        public DelegateCommand PlayButton { get; set; }

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

        //TODO I don't think this is a good way to handle going to the next card
        // It should probably be a method contained in the target object?
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

        public bool IterateSubStep()
        {
            if (_cardCollection[CurrentCardIndex]?.CardStepIndex < _cardCollection[CurrentCardIndex]?.CardSubSteps.Count - 1)
            {
                _cardCollection[CurrentCardIndex]!.CardStepIndex++;
                return true;
            }
            _cardCollection[CurrentCardIndex]!.StepStatus = StepStatus.Completed;
            _cardCollection[CurrentCardIndex]!.StepComplete = true;
            _cardCollection[CurrentCardIndex]!.IsActiveStep = false;
            _cardCollection[CurrentCardIndex]!.CardTime.Pause();
            return false;
        }

        public void PauseCard()
        {
            _cardCollection[CurrentCardIndex]!.StepStatus = StepStatus.Paused;
            _cardCollection[CurrentCardIndex]!.CardTime.Pause();
        }

        public void RetryStep()
        {
            _cardCollection[CurrentCardIndex]!.CardStepIndex = 0;
            _cardCollection[CurrentCardIndex]!.CardTime.Reset();
            _cardCollection[CurrentCardIndex]!.CardTime.Start();
        }

        public void StartCard()
        {
            _cardCollection[CurrentCardIndex]!.IsActiveStep = true;
            _cardCollection[CurrentCardIndex]!.StepStatus = StepStatus.Running;
            _cardCollection[CurrentCardIndex]!.CardTime.Start();
            if (_cardCollection[CurrentCardIndex]!.StepModalData?.IsError == false)
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(_cardCollection[CurrentCardIndex]!.StepModalData!);
            }
        }

        private void CompleteCycle()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            SelectedProgramData!.UpdateAverageCycleTime(CycleTime.TimeSpan);
            CycleCount++;
            SelectedProgramData!.HistoricalCycles = CycleCount;
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Publish();
            if (_cardCollection[CurrentCardIndex]!.StepModalData?.IsError == false)
            {
                _eventAggregator.GetEvent<StartRequest>().Publish();
            }
        }

        private void HandleModalAbortRequest(ModalResponseData obj)
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            _eventAggregator.GetEvent<ProgramDataSaveRequest>().Publish();
        }

        private void HandleModalContinueRequest(ModalResponseData obj)
        {
            //            PlaybackStart();
            IterateSubStep();
        }

        private void HandleModalCustomRequest(ModalResponseData obj)
        {
            // This would probably just raise another event somewhere else
        }

        private void HandleModalRetryRequest(ModalResponseData obj)
        {
            PlaybackStart();
            RetryStep();
        }

        private void HandlePauseConfirmation()
        {
            PauseCard();
        }

        private void HandlePauseRequest()
        {
            PlayBackRunning = false;
            PlayAvailable = true;
            PauseAvailable = false;
            AllowProgramChange = true;
        }

        private void HandleProductImageChangeResponse(BitmapImage? image)
        {
            ProductImage = image;
        }

        private void HandleProgramDataResponse(ObservableCollection<Card?> publishedCardCollection)
        {
            CardCollection = publishedCardCollection;
            CurrentCardIndex = 0;
            CycleCount = SelectedProgramData!.HistoricalCycles;
            PlayAvailable = SelectedProgramData.UserCanStartPlayback;
            _cardCollection[CurrentCardIndex]!.IsActiveStep = true;
            if (SelectedProgramData.AutoStartPlayback)
            {
                _eventAggregator.GetEvent<StartRequest>().Publish();
            }
        }

        private void HandleProgramDataSaveResponse()
        {
            CycleTime.Reset();

            foreach (Card? card in _cardCollection)
            {
                card!.Initialize();
            }
            CurrentCardIndex = 0;
        }

        private void HandleProgramHaltRequest()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
        }

        private void HandleProgramSelectResponse(IProgramData? programData)
        {
            SelectedProgramData = programData!;
        }

        private void HandleRaiseError()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
            CycleTime.Pause();
            if (_cardCollection[CurrentCardIndex]!.StepModalData == null)
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(new ModalData()
                {
                    CanAbort = true,
                    IsError = true,
                    Card = CurrentCard
                });
            }
            else
            {
                _eventAggregator.GetEvent<ModalEvent>().Publish(_cardCollection[CurrentCardIndex]!.StepModalData!);
            }
        }

        private void HandleStartRequest()
        {
            if (PlayBackRunning == false && SelectedProgramData != null)
            {
                PlaybackStart();
            }
        }

        private void PausePressed()
        {
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        private void PlaybackStart()
        {
            PlayAvailable = false;
            PauseAvailable = true;
            CycleTime.Start();
            StartCard();
            PlayBackRunning = true;
            AllowProgramChange = false;
        }

        private void PlayPressed()
        {
            _eventAggregator.GetEvent<StartRequest>().Publish();
        }

        private void RequestProgramSelect()
        {
            _eventAggregator.GetEvent<ProgramSelectRequest>().Publish(SelectedProgramData);
        }

        private void SetCurrentCard()
        {
            CurrentCard = _cardCollection[CurrentCardIndex];
        }

        private void UpdateProductImage()
        {
            ProductImage = SelectedProgramData!.ProductImage;
        }
    }
}