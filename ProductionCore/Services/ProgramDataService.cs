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
        /// Defines the _programRequestOpen.
        /// </summary>
        private bool _programRequestOpen;

        /// <summary>
        /// Defines the _selectedProgramData.
        /// </summary>
        private IProgramData? _selectedProgramData;

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
                SetProperty(ref _selectedProgramData, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestOpen.
        /// </summary>
        public bool ProgramRequestOpen
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
        /// The ProgramSelectRequest.
        /// </summary>
        /// <returns>The <see cref="bool"/>.</returns>
        public bool ProgramSelectRequest()
        {
            ProgramRequestOpen = true;
            return true;
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
    }
}
