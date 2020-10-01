namespace ProdProgramSelect.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

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
        /// Initializes a new instance of the <see cref="ProgramSelectViewModel"/> class.
        /// </summary>
        /// <param name="programDataService">The programDataService<see cref="IProgramDataService"/>.</param>
        public ProgramSelectViewModel(IProgramDataService programDataService)
        {
            _programDataService = programDataService;
            ConfirmButton = new DelegateCommand(ConfirmProgramChange).ObservesCanExecute(() => ProgramDataService.CanConfirm);
            CancelButton = new DelegateCommand(CancelProgramChange).ObservesCanExecute(() => ProgramDataService.CanCancel);
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
            _programDataService.ProgramRequestShow = false;
        }

        /// <summary>
        /// The ConfirmProgramChange.
        /// </summary>
        private void ConfirmProgramChange()
        {
            _programDataService.SetSelectedProgramAsCurrent();
            CleanInstance();
        }
    }
}
