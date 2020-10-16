namespace ProdProgramSelect.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using ProductionCore.Services;

    /// <summary>
    /// Defines the <see cref="ProgramSelectViewModel" />.
    /// </summary>
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
            ConfirmButton = new DelegateCommand(ConfirmProgramChange).ObservesCanExecute(() => ProgramDataService.CanConfirm);
            CancelButton = new DelegateCommand(CancelProgramChange);
        }

        /// <summary>
        /// Gets the ProgramDataService.
        /// </summary>
        public IProgramDataService ProgramDataService
        {
            get
            {
                return _programDataService;
            }
        }

        /// <summary>
        /// Gets the MediationService.
        /// </summary>
        public IMediationService MediationService
        {
            get
            {
                return _mediationService;
            }
        }

        /// <summary>
        /// Gets or sets the ConfirmButton.
        /// </summary>
        public DelegateCommand ConfirmButton { get; set; }

        /// <summary>
        /// Gets or sets the CancelButton.
        /// </summary>
        public DelegateCommand CancelButton { get; set; }

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
            _mediationService.ProgramRequestShow = false;
        }

        /// <summary>
        /// The ConfirmProgramChange.
        /// </summary>
        private void ConfirmProgramChange()
        {
            ProgramDataService.SetSelectedProgramAsCurrent();
            GlobalCommands.RequestProgram.Execute(null);
            CleanInstance();
        }
    }
}
