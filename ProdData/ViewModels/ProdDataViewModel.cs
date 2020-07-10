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

        private ProdDataTimer _cycleTime = new ProdDataTimer();
        public ProdDataTimer CycleTime
        {
            get
            {
                return _cycleTime;
            }
            set
            {
                SetProperty(ref _cycleTime, value);
                RaisePropertyChanged(nameof(CycleTime));
            }
        }

        private ObservableCollection<ProdDataProgramModel> _productionPrograms = new ObservableCollection<ProdDataProgramModel>();
        public ObservableCollection<ProdDataProgramModel> ProductionPrograms
        {
            get
            {
                return _productionPrograms;
            }
            set
            {
                SetProperty(ref _productionPrograms, value);
                RaisePropertyChanged(nameof(ProductionPrograms));
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
                RaisePropertyChanged(nameof(ProcessDisplay));
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
                RaisePropertyChanged(nameof(CycleCount));
            }
        }


        private long _programStep;
        public long ProgramStep
        {
            get
            {
                return _programStep;
            }
            set
            {
                SetProperty(ref _programStep, value);
                RaisePropertyChanged(nameof(ProgramStep));
            }
        }


        private long _programSize;
        public long ProgramSize
        {
            get
            {
                return _programSize;
            }
            set
            {
                SetProperty(ref _programSize, value);
                RaisePropertyChanged(nameof(ProgramSize));
            }
        }

        //private MediaElement _processImage;
        //public MediaElement ProcessImage
        //{
        //    get
        //    {
        //        return _processImage;
        //    }
        //    set
        //    {
        //        SetProperty(ref _processImage, value);
        //        RaisePropertyChanged(nameof(ProcessImage));
        //    }
        //}

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
                RaisePropertyChanged(nameof(ProgramSelectionConfirmationRaised));
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
                RaisePropertyChanged(nameof(PlayBackRunning));
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
                RaisePropertyChanged(nameof(AllowProgramChange));
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
                RaisePropertyChanged(nameof(PlayAvailable));
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
                RaisePropertyChanged(nameof(PauseAvailable));
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
                RaisePropertyChanged(nameof(ProgramName));
            }
        }

        private void VerifyChange(int newProgramSelectionValue)
        {
            if (CycleTime.ElapsedTime.Equals(null))
            {
                _currentProgram = newProgramSelectionValue;
                RaisePropertyChanged(nameof(CurrentProgram));
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
    }
}
