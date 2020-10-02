namespace ProductionCore.Services
{
    using System.Collections.ObjectModel;
    using global::ProductionCore.Interfaces;
    using Prism.Mvvm;

    /// <summary>
    /// Defines the <see cref="ProgramDataService" />.
    /// </summary>
    public class ProgramDataService : BindableBase, IProgramDataService
    {
        /// <summary>
        /// Defines the _programDataFactory.
        /// </summary>
        private readonly IProgramDataFactory _programDataFactory;

        /// <summary>
        /// Defines the _barcodeService.
        /// </summary>
        private readonly IBarcodeService _barcodeService;

        /// <summary>
        /// Defines the _canCancel.
        /// </summary>
        private bool _canCancel;

        /// <summary>
        /// Defines the _canConfirm.
        /// </summary>
        private bool _canConfirm;

        /// <summary>
        /// Defines the _programRequestOpen.
        /// </summary>
        private bool _programRequestOpen;

        /// <summary>
        /// Defines the _selectedProgramData.
        /// </summary>
        private IProgramData? _selectedProgramData;

        /// <summary>
        /// Defines the _currentProgram.
        /// </summary>
        private IProgramData? _currentProgram;

        /// <summary>
        /// Defines the _programList.
        /// </summary>
        private ObservableCollection<IProgramData> _programList = new ObservableCollection<IProgramData>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDataService"/> class.
        /// </summary>
        /// <param name="programDataFactory">The programDataFactory<see cref="IProgramDataFactory"/>.</param>
        /// <param name="barcodeService">The barcodeService<see cref="IBarcodeService"/>.</param>
        public ProgramDataService(IProgramDataFactory programDataFactory, IBarcodeService barcodeService)
        {
            _programDataFactory = programDataFactory;
            _barcodeService = barcodeService;
        }

        /// <summary>
        /// Gets or sets the ProgramList.
        /// </summary>
        public ObservableCollection<IProgramData> ProgramList
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
                SetProperty(ref _selectedProgramData, value, SetConfirmStatus);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestShow.
        /// </summary>
        public bool ProgramRequestShow
        {
            get
            {
                return _programRequestOpen;
            }

            set
            {
                SetProperty(ref _programRequestOpen, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanCancel.
        /// </summary>
        public bool CanCancel
        {
            get
            {
                return _canCancel;
            }

            set
            {
                SetProperty(ref _canCancel, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether CanConfirm.
        /// </summary>
        public bool CanConfirm
        {
            get
            {
                return _canConfirm;
            }

            private set
            {
                SetProperty(ref _canConfirm, value);
            }
        }

        /// <summary>
        /// Gets or sets the CurrentProgram.
        /// </summary>
        public IProgramData? CurrentProgram
        {
            get
            {
                return _currentProgram;
            }

            set
            {
                SetProperty(ref _currentProgram, value, SetCancelStatus);
            }
        }

        /// <summary>
        /// The UpdateProgramCycleTime.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        /// <param name="cycles">The cycles<see cref="long"/>.</param>
        public void UpdateProgramCycleTime(IProgramData? program, long cycles)
        {
            if (program == null)
            {
                program = CurrentProgram ?? null;
                if (program == null)
                {
                    return;
                }
            }

            program.Cycles = cycles;
        }

        /// <summary>
        /// The ProgramSelectClose.
        /// </summary>
        public void ProgramSelectClose()
        {
            _programRequestOpen = false;
        }

        /// <summary>
        /// The Program.
        /// </summary>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        public IProgramData CreateProgram()
        {
            return _programDataFactory.Create();
        }

        /// <summary>
        /// The CreateBarcode.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode CreateBarcode()
        {
            return _barcodeService.CreateBarcode();
        }

        /// <summary>
        /// The SetSelectedProgramAsCurrent.
        /// </summary>
        public void SetSelectedProgramAsCurrent()
        {
            CurrentProgram = SelectedProgramData;
        }

        /// <summary>
        /// The LoadProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void LoadProgram(IProgramData? program = null)
        {
            if (program == null && CurrentProgram == null)
            {
                return;
            }
            else if (program == null)
            {
                program = CurrentProgram ?? null;
            }
            //// do something here... load the program, which currently is the job of the testgeneratorsingleton class
            //// not sure what to do, since it should be abstracted somehow...
        }

        /// <summary>
        /// The SaveProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        public void SaveProgram(IProgramData? program = null)
        {
            if (program == null && CurrentProgram == null)
            {
                return;
            }
            else if (program == null)
            {
                program = CurrentProgram ?? null;
            }
            //// do something here... save the program, which currently is the job of the testgeneratorsingleton class
            //// not sure what to do, since it should be abstracted somehow...
        }

        /// <summary>
        /// The SetCancelStatus.
        /// </summary>
        private void SetCancelStatus()
        {
            if (_currentProgram == null)
            {
                CanCancel = false;
            }
            else
            {
                CanCancel = true;
            }
        }

        /// <summary>
        /// The SetConfirmStatus.
        /// </summary>
        private void SetConfirmStatus()
        {
            if (_selectedProgramData == null)
            {
                CanConfirm = false;
            }
            else
            {
                CanConfirm = true;
            }
        }
    }
}
