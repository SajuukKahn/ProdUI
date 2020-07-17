using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using ProdData.Events;
using ProdData.Extensions;
using ProdData.Models;
using System;
using System.Windows.Media.Imaging;

namespace ProdData.ViewModels
{
    public class ProdDataViewModel : BindableBase
    {
        private bool _allowProgramChange = true;

        private IndexedObservableCollection<Card> _cardCollection = new IndexedObservableCollection<Card>();
        private Card _currentCard;
        private long _cycleCount;
        private Timer _cycleTime = new Timer();
        private IEventAggregator _eventAggregator;

        private ProgramID _oldSelectedProgramData;
        private bool _pauseAvailable;
        private bool _playAvailable;
        private bool _playBackRunning;
        private BitmapImage? _processDisplay;
        private IndexedObservableCollection<ProgramID> _programList = new IndexedObservableCollection<ProgramID>();
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
            ConfirmButton = new DelegateCommand(ConfirmProgramChange);
            CancelButton = new DelegateCommand(CancelProgramChange);
            _eventAggregator.GetEvent<ProgramNamesResponse>().Subscribe(HandleProgramNamesResponse);
            _eventAggregator.GetEvent<ProgramDataResponse>().Subscribe(HandleProgramDataResponse);
            _eventAggregator.GetEvent<StartRequest>().Subscribe(FulfillStartRequest);
            _eventAggregator.GetEvent<StartResponse>().Subscribe(HandleStartResponse);
            _eventAggregator.GetEvent<PauseRequest>().Subscribe(FulfillPauseRequest);
            _eventAggregator.GetEvent<PauseResponse>().Subscribe(HandlePauseResponse);
            _eventAggregator.GetEvent<ProcessDisplayChangeRequest>().Subscribe(FulfillProcessDisplayChangeRequest);
            _eventAggregator.GetEvent<ProcessDisplayChangeResponse>().Subscribe(HandleProcessDisplayChangeResponse);
            _eventAggregator.GetEvent<RaiseError>().Subscribe(HandleError);
            _eventAggregator.GetEvent<AdvanceStep>().Subscribe(Next);
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

        public IndexedObservableCollection<Card> CardCollection
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

        public IndexedObservableCollection<ProgramID> ProgramList
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
            PlayBackRunning = false;
            AllowProgramChange = true;
            RequestCards();
            CycleTime.Reset();
            PlayAvailable = true;
            PauseAvailable = false;
        }

        public void Next()
        {
            if (SubStep < _cardCollection[CurrentCard.Ordinal]?.CardSubSteps.Count)
            {
            }
            else if (CurrentCard.Ordinal < _cardCollection?.Count)
            {
                // need to add in some stuff here... changing the "status" of the card, for instance
                _cardCollection[CurrentCard.Ordinal].StepStatus = StepStatus.Completed;
                _cardCollection[CurrentCard.Ordinal].StepComplete = true;
                _cardCollection[CurrentCard.Ordinal].StepPassed = true;
                _cardCollection[CurrentCard.Ordinal].StepComplete = true;
                _cardCollection[CurrentCard.Ordinal].IsActiveStep = false;
                CurrentCard = _cardCollection[CurrentCard.Ordinal + 1];
                _cardCollection[CurrentCard.Ordinal].IsActiveStep = true;
                _cardCollection[CurrentCard.Ordinal].StepStatus = StepStatus.Running;
            }
            else
            {
                //Deck is completed - what do
            }
        }

        private void CancelProgramChange()
        {
            AllowProgramChange = true;
            PlayAvailable = true;
            SelectedProgramData = _oldSelectedProgramData;
            ProgramSelectionConfirmationRaised = false;
        }

        private void ConfirmProgramChange()
        {
            LoadProductionDeck();
            PlayAvailable = true;
            ProgramSelectionConfirmationRaised = false;
        }

        private void FulfillPauseRequest()
        {
        }

        private void FulfillProcessDisplayChangeRequest()
        {
        }

        private void FulfillStartRequest()
        {
        }

        private void HandleError()
        {
        }

        private void HandlePauseResponse()
        {
        }

        private void HandleProcessDisplayChangeResponse()
        {
        }

        private void HandleProgramDataResponse(IndexedObservableCollection<Card> publishedCardCollection)
        {
            _cardCollection.Clear();
            CardCollection = publishedCardCollection;
        }

        private void HandleProgramNamesResponse(IndexedObservableCollection<ProgramID> publishedProgramList)
        {
            _programList.Clear();
            ProgramList = publishedProgramList;
        }

        private void HandleStartResponse()
        {
        }

        private void PausePressed()
        {
            PlayBackRunning = false;
            CycleTime.Pause();
            PlayAvailable = true;
            PauseAvailable = false;
        }

        private void PlayPressed()
        {
            if (PlayBackRunning == false)
            {
                AllowProgramChange = false;
                PlayAvailable = false;
                PauseAvailable = true;
                CurrentCard = _retainedCard;
                SubStep = _retainedSubStep;
                CycleTime.Start();
            }
            PlayBackRunning = true;
        }

        private void RequestCards()
        {
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish();
        }

        private void RequestPrograms()
        {
            _eventAggregator.GetEvent<ProgramNamesRequest>().Publish();
        }

        private void RetainStep()
        {
            if (PlayBackRunning)
            {
                _retainedCard = CurrentCard;
            }
            SubStep = 0;
        }

        private void RetainSubStep()
        {
            if (PlayBackRunning)
            {
                _retainedSubStep = _subStep;
            }
        }

        private void VerifyChange()
        {
            if (CycleTime?.ElapsedTime == null)
            {
                ProcessDisplay = _programList[SelectedProgramData.Ordinal].ProductImage;
                LoadProductionDeck();
                return;
            }
            PauseAvailable = false;
            PlayAvailable = false;
            AllowProgramChange = false;
            ProgramSelectionConfirmationRaised = true;
        }

        // Events
        //   ProgramDataRequest
        //   ProgramDataResponse
        //   ProgramNamesRequest
        //   ProgramNamesResponse
        //   StartRequest
        //   StartResponse
        //   PauseRequest
        //   PauseResponse
        //   ProcessDisplayChangeRequest
        //   ProcessDisplayChangeResponse
        //   RaiseError
        //   AdvanceStep

        // when play is hit, we need to go back to the 'current' card and the 'current' sub-step and then raise the 'play' event and then start the timer
    }
}