using Prism.Commands;
using Prism.Mvvm;
using ProdData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ProdData.ViewModels
{
    public class ProdDataViewModel : BindableBase
    {
        public DelegateCommand PlayButton { get; set; }
        public DelegateCommand PauseButton { get; set; }
        public DelegateCommand ConfirmButton { get; set; }
        public DelegateCommand CancelButton { get; set; }

        public ProdDataViewModel()
        { 
            PlayButton = new DelegateCommand(PlayPressed).ObservesCanExecute(() => PlayAvailable);
            PauseButton = new DelegateCommand(PausePressed).ObservesCanExecute(() => PauseAvailable);
            ConfirmButton = new DelegateCommand(ConfirmProgramChange);
            CancelButton = new DelegateCommand(CancelProgramChange);
            PlayAvailable = true;
            PauseAvailable = false;
            ProgramSelectionConfirmationRaised = true;
        }

        private ObservableCollection<ProgramModel> _productionPrograms = new ObservableCollection<ProgramModel>();
        public ObservableCollection<ProgramModel> ProductionPrograms
        {
            get
            {
                return _productionPrograms;
            }
            set
            {
                SetProperty(ref _productionPrograms, value);
            }
        }

        private ObservableCollection<ProgramModel> _productionProgramList = new ObservableCollection<ProgramModel>();
        public ObservableCollection<ProgramModel> ProductionProgramList
        {
            get
            {
                return _productionProgramList;
            }
            set
            {
                SetProperty(ref _productionProgramList, value);
            }
        }

        private ObservableCollection<CardModel> _productionCardCollection = new ObservableCollection<CardModel>();
        public ObservableCollection<CardModel> ProductionCardCollection
        {
            get
            {
                return _productionCardCollection;
            }
            set
            {
                SetProperty(ref _productionCardCollection, value);
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

        private ImageSource _processDisplay;
        public ImageSource ProcessDisplay
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

        private string _oldProgramName;
        private string _programName;
        public string ProgramName
        {
            get
            {
                return _programName;
            }
            set
            {
                _oldProgramName = _programName;
                SetProperty(ref _programName, value);
            }
        }

        private void VerifyChange(int newProgramSelectionValue)
        {
            if (CycleTime.ElapsedTime.Equals(null))
            {
                SetProperty(ref _currentProgram, newProgramSelectionValue);
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
            ProgramName = _oldProgramName;
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
            if(SubStep < _productionCardCollection[ProgramStep].CardSubSteps.Count)
            {

            }
            else if (ProgramStep < _productionCardCollection.Count)
            {
                // need to add in some stuff here... changing the "status" of the card, for instance
                _productionCardCollection[ProgramStep].StepStatus = CardStepStatus.Completed;
                _productionCardCollection[ProgramStep].StepComplete = true;
                _productionCardCollection[ProgramStep].StepPassed = stepPassed;
                _productionCardCollection[ProgramStep].StepComplete = stepComplete;
                _productionCardCollection[ProgramStep].IsActiveStep = false;
                ProgramStep++;
                _productionCardCollection[ProgramStep].IsActiveStep = true;
                _productionCardCollection[ProgramStep].StepStatus = CardStepStatus.Running;
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
