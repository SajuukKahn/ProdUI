using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProdData.Events;
using ProdData.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Media.Imaging;

namespace ProdData.ViewModels
{
    public class ProdDataViewModel : BindableBase
    {
        private bool _allowProgramChange = true;

        private ObservableCollection<Card> _cardCollection = new ObservableCollection<Card>();
        private Card _currentCard;
        private int _currentCardIndex;
        private long _cycleCount;
        private Timer _cycleTime = new Timer();
        private bool _debugEnabled = true;
        private IEventAggregator _eventAggregator;
        private ProgramID _oldSelectedProgramData;
        private bool _pauseAvailable;
        private bool _playAvailable;
        private bool _playBackRunning;
        private BitmapImage? _processDisplay;
        private ObservableCollection<ProgramID> _programList = new ObservableCollection<ProgramID>();
        private bool _programSelectionConfirmationRaised;
        private Card _retainedCard;
        private int _retainedSubStep;
        private ProgramID _selectedProgramData;
        private int _subStep;

        public ProdDataViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            PlayButton = new DelegateCommand(PlayPressed).ObservesCanExecute(() => PlayAvailable);
            PauseButton = new DelegateCommand(PausePressed).ObservesCanExecute(() => PauseAvailable);
            InitiateProgramListRequest = new DelegateCommand(RequestPrograms);
            ConfirmButton = new DelegateCommand(ConfirmProgramChange); // needs to go - refactor to modal popup confirmation dialog (link in notes to SO.com)
            CancelButton = new DelegateCommand(CancelProgramChange); // needs to go
            _eventAggregator.GetEvent<ProgramNamesResponse>().Subscribe(HandleProgramNamesResponse);
            _eventAggregator.GetEvent<ProgramDataResponse>().Subscribe(HandleProgramDataResponse);
            _eventAggregator.GetEvent<StartRequest>().Subscribe(FulfillStartRequest);
            _eventAggregator.GetEvent<ProgramPaused>().Subscribe(PauseCardTimer);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(FulfillPauseRequest);
            _eventAggregator.GetEvent<ProcessDisplayChangeResponse>().Subscribe(HandleProcessDisplayChangeResponse);
            _eventAggregator.GetEvent<RaiseError>().Subscribe(HandleError);
            _eventAggregator.GetEvent<AdvanceStep>().Subscribe(Next);
        }

        private bool _programChangeVolatile;
        public bool ProgramChangeVolatile
        {
            get 
            { 
                return _programChangeVolatile; 
            }
            set 
            { 
                SetProperty(ref _programChangeVolatile, value); 
            }
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

        public DelegateCommand CancelButton { get; set; }

        public ObservableCollection<Card> CardCollection
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

        public DelegateCommand ConfirmButton { get; set; }

        public Card CurrentCard
        {
            get
            {
                return _currentCard;
            }
            set
            {
                SetProperty(ref _currentCard, value, RetainStep);
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
                SetProperty(ref _currentCardIndex, value);
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

        public DelegateCommand InitiateProgramListRequest { get; set; }

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

        public BitmapImage? ProcessDisplay
        {
            get
            {
                return _processDisplay;
            }
            set
            {
                SetProperty(ref _processDisplay, value);
            }
        }

        public ObservableCollection<ProgramID> ProgramList
        {
            get
            {
                return _programList;
            }
            set
            {
                SetProperty(ref _programList, value);
            }
        }

        public bool ProgramSelectionConfirmationRaised
        {
            get
            {
                return _programSelectionConfirmationRaised;
            }
            set
            {
                SetProperty(ref _programSelectionConfirmationRaised, value);
            }
        }

        public ProgramID SelectedProgramData
        {
            get
            {
                return _selectedProgramData;
            }
            set
            {
                _oldSelectedProgramData = _selectedProgramData;
                SetProperty(ref _selectedProgramData, value, VerifyChange);
            }
        }

        public int SubStep
        {
            get
            {
                return _subStep;
            }
            set
            {
                SetProperty(ref _subStep, value, RetainSubStep);
            }
        }

        public void LoadProductionDeck()
        {
            DebugLogCaller();
            PlayBackRunning = false;
            AllowProgramChange = true;
            RequestCards();
            CycleTime.Reset();
            PlayAvailable = true;
            PauseAvailable = false;
        }

        public void Next()
        {
            DebugLogCaller();
            if (SubStep < CurrentCard.CardSubSteps?.Count)
            {
                SubStep++;
            }
            else if (_currentCardIndex < _cardCollection?.Count)
            {
                SubStep = 0;

                _cardCollection[_currentCardIndex].StepStatus = StepStatus.Completed;
                _cardCollection[_currentCardIndex].StepComplete = true;
                _cardCollection[_currentCardIndex].IsActiveStep = false;
                CurrentCard = _cardCollection[_currentCardIndex + 1];
                _cardCollection[_currentCardIndex].IsActiveStep = true;
                _cardCollection[_currentCardIndex].StepStatus = StepStatus.Running;
            }
            else
            {
                SubStep = 0;
                _eventAggregator.GetEvent<PauseRequest>().Publish();

                foreach (var Card in _cardCollection)
                {
                    Card.StepStatus = StepStatus.Waiting;
                    Card.StepComplete = false;
                    Card.IsActiveStep = false;
                }
                ProgramChangeVolatile = false;
                CurrentCard = _cardCollection[0];
            }
        }

        private void CancelProgramChange()
        {
            DebugLogCaller();
            AllowProgramChange = true;
            PlayAvailable = true;
            SelectedProgramData = _oldSelectedProgramData;
            ProgramSelectionConfirmationRaised = false;
        }

        private void ConfirmProgramChange()
        {
            DebugLogCaller();
            LoadProductionDeck();
            PlayAvailable = true;
            ProgramSelectionConfirmationRaised = false;
        }

        private void DebugLogCaller([CallerMemberName] string caller = null)
        {
            if (!_debugEnabled)
            {
                return;
            }
            Debug.WriteLine(this.ToString() + "\t|\t" + caller);
        }

        private void FulfillPauseRequest()
        {
            DebugLogCaller();
            PlayBackRunning = false;
            PlayAvailable = true;
            PauseAvailable = false;
        }

        private void FulfillStartRequest()
        {
            if (PlayBackRunning == false)
            {
                AllowProgramChange = false;
                PlayAvailable = false;
                PauseAvailable = true;
                CurrentCard = _retainedCard;
                SubStep = _retainedSubStep;
                CycleTime.Start();
                CurrentCard.CardTime.Start();
            }
            ProgramChangeVolatile = true;
            PlayBackRunning = true;
        }

        private void HandleError()
        {
            DebugLogCaller();
        }

        private void HandleProcessDisplayChangeResponse(BitmapImage? image)
        {
            DebugLogCaller();
            ProcessDisplay = image;
        }

        private void HandleProgramDataResponse(ObservableCollection<Card> publishedCardCollection)
        {
            DebugLogCaller();
            _cardCollection.Clear();
            CardCollection = publishedCardCollection;
            CurrentCard = _cardCollection?[0];
        }

        private void HandleProgramNamesResponse(ObservableCollection<ProgramID> publishedProgramList)
        {
            DebugLogCaller();
            _programList.Clear();
            ProgramList = publishedProgramList;
        }

        private void PauseCardTimer()
        {
            CurrentCard.CardTime.Pause();
        }

        private void PausePressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<PauseRequest>().Publish();
        }

        private void PlayPressed()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<StartRequest>().Publish();
        }

        private void RequestCards()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish();
        }

        private void RequestPrograms()
        {
            DebugLogCaller();
            _eventAggregator.GetEvent<ProgramNamesRequest>().Publish();
        }

        private void RetainStep()
        {
            DebugLogCaller();
            if (PlayBackRunning)
            {
                CurrentCardIndex = _cardCollection.IndexOf(CurrentCard);
                _retainedCard = CurrentCard;
            }
            SubStep = 0;
        }

        private void RetainSubStep()
        {
            DebugLogCaller();
            if (PlayBackRunning)
            {
                _retainedSubStep = _subStep;
            }
        }

        private void VerifyChange()
        {
            DebugLogCaller();
            if (CycleTime?.ElapsedTime == null)
            {
                ProcessDisplay = _programList[_programList.IndexOf(SelectedProgramData)].ProductImage;
                LoadProductionDeck();
                return;
            }
            PauseAvailable = false;
            PlayAvailable = false;
            AllowProgramChange = false;
            ProgramSelectionConfirmationRaised = true;
        }
    }
}