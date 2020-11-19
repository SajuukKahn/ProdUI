namespace ProdProgramSelect.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    internal class ProgramSelectViewModel : BindableBase, IProgramSelectViewModel
    {
        /// <summary>
        /// Defines the _programDataService.
        /// </summary>
        private readonly IProgramDataService _programDataService;

        /// <summary>
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramSelectViewModel"/> class.
        /// </summary>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        public ProgramSelectViewModel(IProgramDataService programDataService, IMediationService mediationService)
        {
            _programDataService = programDataService;
            _mediationService = mediationService;
            ConfirmCommand = new DelegateCommand(Confirm).ObservesCanExecute(() => ProgramDataService.CanConfirm);
            CancelCommand = new DelegateCommand(Cancel);
        }

        /// <inheritdoc/>
        public IProgramDataService ProgramDataService
        {
            get
            {
                return _programDataService;
            }
        }

        /// <inheritdoc/>
        public IMediationService MediationService
        {
            get
            {
                return _mediationService;
            }
        }

        /// <inheritdoc/>
        public DelegateCommand ConfirmCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// The CancelProgramChange.
        /// </summary>
        private void Cancel()
        {
            CleanInstance();
        }

        /// <summary>
        /// The CleanInstance.
        /// </summary>
        private void CleanInstance()
        {
            _mediationService.ProgramRequestShow = false;
        }

        /// <summary>
        /// The ConfirmProgramChange.
        /// </summary>
        private void Confirm()
        {
            ProgramDataService.SetSelectedProgramAsCurrent();
            CleanInstance();
        }
    }
}
