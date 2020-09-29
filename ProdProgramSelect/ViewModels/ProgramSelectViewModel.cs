namespace ProdProgramSelect.ViewModels
{
    using System.Collections.ObjectModel;
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
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _canCancel.
        /// </summary>
        private bool _canCancel;

        /// <summary>
        /// Defines the _canConfirm.
        /// </summary>
        private bool _canConfirm;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramSelectViewModel"/> class.
        /// </summary>
        /// <param name="eventAggregator">The eventAggregator<see cref="IEventAggregator"/>.</param>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        public ProgramSelectViewModel(IEventAggregator eventAggregator, IProgramDataService programDataService)
        {
            _programDataService = programDataService;
            _eventAggregator = eventAggregator;
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
        /// Gets the ProgramList
        /// Gets or sets the ProgramList....
        /// </summary>
        public ObservableCollection<IProgramData> ProgramList
        {
            get
            {
                return _programDataService.ProgramList;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestOpen
        /// Gets a value indicating whether ProgramRequestOpen..
        /// </summary>
        public bool ProgramRequestOpen
        {
            get
            {
                return _programDataService.ProgramRequestOpen;
            }

            set
            {
                RaisePropertyChanged(nameof(ProgramRequestOpen));
            }
        }

        /// <summary>
        /// Gets or sets the SelectedProgramData.
        /// </summary>
        public IProgramData? SelectedProgramData
        {
            get
            {
                return _programDataService.SelectedProgramData;
            }

            set
            {
                _programDataService.SelectedProgramData = value;
                SetCanConfirm();
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
            _programDataService.ProgramSelectClose();
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
        /// The SetCanConfirm.
        /// </summary>
        private void SetCanConfirm()
        {
            CanConfirm = true;
        }
    }
}
