using Prism.Commands;
using Prism.Mvvm;
using ProdData.Models;
using ProdData.Events;
using System.Collections.ObjectModel;
using System.Windows.Media;
using Prism.Events;
using System;
using System.Windows.Media.Imaging;

namespace ProdData.ViewModels
{
    public class ProdDataViewModel : BindableBase
    {
        public DelegateCommand PlayButton { get; set; }
        public DelegateCommand PauseButton { get; set; }
        public DelegateCommand ConfirmButton { get; set; }
        public DelegateCommand CancelButton { get; set; }
        public DelegateCommand InitiateProgramListRequest { get; set; }

        private IEventAggregator _eventAggregator;
        public ProdDataViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            PlayButton = new DelegateCommand(PlayPressed).ObservesCanExecute(() => PlayAvailable);
            PauseButton = new DelegateCommand(PausePressed).ObservesCanExecute(() => PauseAvailable);
            InitiateProgramListRequest = new DelegateCommand(RequestPrograms);
            ConfirmButton = new DelegateCommand(ConfirmProgramChange);
            CancelButton = new DelegateCommand(CancelProgramChange);
            _eventAggregator.GetEvent<ProgramNamesResponse>().Subscribe(SetProgramList);
            _eventAggregator.GetEvent<ProgramDataResponse>().Subscribe(SetCardData);
        }

        private void RequestPrograms()
        {
            _eventAggregator.GetEvent<ProgramNamesRequest>().Publish();
        }

        private void RequestCards()
        {
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish();
        }

        private void SetCardData(ObservableCollection<Card> publishedCardCollection)
        {
            _cardCollection.Clear();
            CardCollection = publishedCardCollection;
        }

        private void SetProgramList(ObservableCollection<ProgramID> publishedProgramList)
        {
            _programList.Clear();
            ProgramList = publishedProgramList;
        }

        private ObservableCollection<ProgramID> _programList = new ObservableCollection<ProgramID>();
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

        private ObservableCollection<Card> _cardCollection = new ObservableCollection<Card>();
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


        private Timer _cycleTime = new Timer();
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

        private BitmapImage? _processDisplay;
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

        private long _cycleCount;
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

        private bool _programSelectionConfirmationRaised;
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

        private bool _playBackRunning;
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

        private bool _allowProgramChange = true;
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

        private bool _playAvailable;
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

        private bool _pauseAvailable;
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
        private int _newProgramSelection = -1;
        private int _currentProgram = -1;
        public int CurrentProgram
        {
            get
            {
                return _currentProgram;
            }
            set
            {
                if (_currentProgram != value)
                {
                    VerifyChange(value);
                }
            }
        }

        private ProgramID _oldSelectedProgramData;
        private ProgramID _selectedProgramData;
        public ProgramID SelectedProgramData
        {
            get
            {
                return _selectedProgramData;
            }
            set
            {
                _oldSelectedProgramData = _selectedProgramData;
                SetProperty(ref _selectedProgramData, value);
            }
        }


        private void VerifyChange(int newProgramSelectionValue)
        {
            if (CycleTime?.ElapsedTime == null)
            {
                SetProperty(ref _currentProgram, newProgramSelectionValue);
                ProcessDisplay = _programList[_currentProgram].ProductImage;
                LoadProductionDeck();
                _newProgramSelection = -1;
                return;
            }

            PauseAvailable = false;
            PlayAvailable = false;
            AllowProgramChange = false;
            ProgramSelectionConfirmationRaised = true;
            _newProgramSelection = newProgramSelectionValue;
        }

        private void ConfirmProgramChange()
        {
            _currentProgram = _newProgramSelection;
            RaisePropertyChanged(nameof(CurrentProgram));
            LoadProductionDeck();
            PlayAvailable = true;
            ProgramSelectionConfirmationRaised = false;
            _newProgramSelection = -1;
        }

        private void CancelProgramChange()
        {
            _newProgramSelection = -1;
            AllowProgramChange = true;
            CurrentProgram = _currentProgram;
            PlayAvailable = true;
            SelectedProgramData = _oldSelectedProgramData;
            ProgramSelectionConfirmationRaised = false;
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
                ProgramStep = _retainedProgramStep;
                SubStep = _retainedSubStep;
                CycleTime.Start();
            }
            PlayBackRunning = true;
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


        private int _retainedProgramStep;
        private int _programStep;
        public int ProgramStep
        {
            get
            {
                return _programStep;
            }
            set
            {
                SetProperty(ref _programStep, value, RetainStep);
            }
        }

        private void RetainStep()
        {
            if(PlayBackRunning)
            { 
                _retainedProgramStep = _programStep;
            }
            SubStep = 0;
        }

        private int _retainedSubStep;
        private int _subStep;
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

        private void RetainSubStep()
        {
            if (PlayBackRunning)
            {
                _retainedSubStep = _subStep;
            }
        }

        public void Next(bool stepPassed = true, bool stepComplete = true)
        {
            if(SubStep < _cardCollection[ProgramStep].CardSubSteps.Count)
            {

            }
            else if (ProgramStep < _cardCollection.Count)
            {
                // need to add in some stuff here... changing the "status" of the card, for instance
                _cardCollection[ProgramStep].StepStatus = StepStatus.Completed;
                _cardCollection[ProgramStep].StepComplete = true;
                _cardCollection[ProgramStep].StepPassed = stepPassed;
                _cardCollection[ProgramStep].StepComplete = stepComplete;
                _cardCollection[ProgramStep].IsActiveStep = false;
                ProgramStep++;
                _cardCollection[ProgramStep].IsActiveStep = true;
                _cardCollection[ProgramStep].StepStatus = StepStatus.Running;
            }
            else
            {
                //Deck is completed - what do
            }
        }

        // Events
        //  Outgoing
        //   Program completed//   Program completed - restart?
        //   Program selected / changed - comes from somewhere else
        //   Paused / pausing AC - card timer needs to pause
        //   Error reached
        //   Card Timer paused
        //   Start program
        //   Pause program
        //
        //  Incoming
        //   Display Image change
        //   Start program
        //   Pause program

    }
}
