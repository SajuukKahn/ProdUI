namespace ProdProgramSelect.Services
{
    using System.Collections.ObjectModel;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProgramDataService" />.
    /// </summary>
    public class ProgramDataService : BindableBase, IProgramDataService
    {
        /// <summary>
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

        /// <summary>
        /// Defines the _canConfirm.
        /// </summary>
        private bool _canConfirm;

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
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        public ProgramDataService(IMediationService mediationService)
        {
            _mediationService = mediationService;
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
        /// Gets a value indicating whether CanCancel.
        /// </summary>
        public bool CanCancel
        {
            get
            {
                return _mediationService.CurrentProgram != null;
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
        /// The ProgramSelectClose.
        /// </summary>
        public void ProgramSelectClose()
        {
            _mediationService.ProgramRequestShow = false;
        }

        /// <summary>
        /// The SetSelectedProgramAsCurrent.
        /// </summary>
        public void SetSelectedProgramAsCurrent()
        {
            _mediationService.CurrentProgram = SelectedProgramData;
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
