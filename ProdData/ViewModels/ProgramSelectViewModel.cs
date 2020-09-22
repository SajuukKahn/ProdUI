namespace ProdData.ViewModels
{
    using Prism.Commands;
    using Prism.Events;
    using Prism.Mvvm;
    using ProductionCore.Events;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProgramSelectViewModel" />.
    /// </summary>
    internal class ProgramSelectViewModel : BindableBase, IProgramSelectViewModel
    {
        /// <summary>
        /// Defines the _eventAggregator.
        /// </summary>
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Defines the _canCancel.
        /// </summary>
        private bool _canCancel;

        /// <summary>
        /// Defines the _canConfirm.
        /// </summary>
        private bool _canConfirm;

        /// <summary>
        /// Defines the _oldSelectedProgramData.
        /// </summary>
        private IProgramData? _oldSelectedProgramData;

        /// <summary>
        /// Defines the _programList.
        /// </summary>
        private IProgramCollection? _programList;

        /// <summary>
        /// Defines the _programReqestOpen.
        /// </summary>
        private bool _programReqestOpen = false;

        /// <summary>
        /// Defines the _requestAwaiting.
        /// </summary>
        private bool _requestAwaiting = true;

        /// <summary>
        /// Defines the _selectedProgramData.
        /// </summary>
        private IProgramData? _selectedProgramData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramSelectViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="programCollection">The programCollection<see cref="IProgramCollection?"/>.</param>
        public ProgramSelectViewModel(IEventAggregator eventAggregator, IProgramCollection? programCollection)
        {
            _programList = programCollection;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<ProgramNamesResponse>().Subscribe(HandleProgramNamesResponse);
            _eventAggregator.GetEvent<ProgramSelectRequest>().Subscribe(HandleProgramSelectRequest);
            ConfirmButton = new DelegateCommand(ConfirmProgramChange).ObservesCanExecute(() => CanConfirm);
            CancelButton = new DelegateCommand(CancelProgramChange).ObservesCanExecute(() => CanCancel);
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
        /// Gets or sets the CancelButton.
        /// </summary>
        public DelegateCommand CancelButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanConfirm.
        /// </summary>
        public bool CanConfirm
        {
            get
            {
                return _canConfirm;
            }

            set
            {
                SetProperty(ref _canConfirm, value);
            }
        }

        /// <summary>
        /// Gets or sets the ConfirmButton.
        /// </summary>
        public DelegateCommand ConfirmButton { get; set; }

        /// <summary>
        /// Gets or sets the ProgramList.
        /// </summary>
        public IProgramCollection? ProgramList
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
        /// Gets or sets a value indicating whether ProgramRequestOpen.
        /// </summary>
        public bool ProgramRequestOpen
        {
            get
            {
                return _programReqestOpen;
            }

            set
            {
                SetProperty(ref _programReqestOpen, value, RequestPrograms);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether RequestAwaiting.
        /// </summary>
        public bool RequestAwaiting
        {
            get
            {
                return _requestAwaiting;
            }

            set
            {
                SetProperty(ref _requestAwaiting, value);
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
                SetProperty(ref _selectedProgramData, value, SetCanConfirm);
            }
        }

        /// <summary>
        /// The CancelProgramChange.
        /// </summary>
        private void CancelProgramChange()
        {
            CleanInstance();
        }

        /// <summary>
        /// The CleanInstance.
        /// </summary>
        private void CleanInstance()
        {
            CanConfirm = false;
            CanCancel = false;
            ProgramRequestOpen = false;
            _oldSelectedProgramData = null;
        }

        /// <summary>
        /// The ConfirmProgramChange.
        /// </summary>
        private void ConfirmProgramChange()
        {
            _eventAggregator.GetEvent<ProgramSelectResponse>().Publish(SelectedProgramData);
            _eventAggregator.GetEvent<ProgramDataRequest>().Publish(SelectedProgramData);
            CleanInstance();
        }

        /// <summary>
        /// The HandleProgramNamesResponse.
        /// </summary>
        private void HandleProgramNamesResponse()
        {
            RequestAwaiting = false;
            ProgramList = _programList;
        }

        /// <summary>
        /// The HandleProgramSelectRequest.
        /// </summary>
        /// <param name="oldProgramData">The oldProgramData<see cref="IProgramData?"/>.</param>
        private void HandleProgramSelectRequest(IProgramData? oldProgramData)
        {
            _oldSelectedProgramData = oldProgramData;
            if (_oldSelectedProgramData != null)
            {
                CanCancel = true;
                RequestAwaiting = false;
            }

            ProgramRequestOpen = true;
        }

        /// <summary>
        /// The RequestPrograms.
        /// </summary>
        private void RequestPrograms()
        {
            if (_programList?.ProgramList == null || _programList?.ProgramList?.Count < 1)
            {
                _eventAggregator.GetEvent<ProgramNamesRequest>().Publish();
            }
        }

        /// <summary>
        /// The SetCanConfirm.
        /// </summary>
        private void SetCanConfirm()
        {
            CanConfirm = true;
        }
    }
}
