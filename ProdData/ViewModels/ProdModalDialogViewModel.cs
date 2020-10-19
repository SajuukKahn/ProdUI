namespace ProdData.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="ProdModalDialogViewModel" />.
    /// </summary>
    public class ProdModalDialogViewModel : BindableBase, IProdModalDialogViewModel
    {
        /// <summary>
        /// Defines the _modalService.
        /// </summary>
        private readonly IModalService _modalService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdModalDialogViewModel"/> class.
        /// </summary>
        /// <param name="modalService">The modalService<see cref="IModalService"/>.</param>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public ProdModalDialogViewModel(IModalService modalService, IPlaybackService playbackService)
        {
            _modalService = modalService;

            Abort = new DelegateCommand(() =>
            {
                playbackService.Abort();
                ModalService.CloseModal();
            }).ObservesCanExecute(() => ModalService.ActiveModalData!.CanAbort);

            Continue = new DelegateCommand(() =>
            {
                playbackService.AdvanceStep();
                ModalService.CloseModal();
            }).ObservesCanExecute(() => ModalService.ActiveModalData!.CanContinue);

            Retry = new DelegateCommand(() =>
            {
                playbackService.RetryCard();
                ModalService.CloseModal();
            }).ObservesCanExecute(() => ModalService.ActiveModalData!.CanRetry);

            Custom = new DelegateCommand(() =>
            {
                playbackService.Abort();
                ModalService.CloseModal();
            }).ObservesCanExecute(() => ModalService.ActiveModalData!.CanCustom);
        }

        /// <summary>
        /// Gets the ModalService.
        /// </summary>
        public IModalService ModalService
        {
            get
            {
                return _modalService;
            }
        }

        /// <summary>
        /// Gets or sets the Abort.
        /// </summary>
        public DelegateCommand Abort { get; set; }

        /// <summary>
        /// Gets or sets the Continue.
        /// </summary>
        public DelegateCommand Continue { get; set; }

        /// <summary>
        /// Gets or sets the Custom.
        /// </summary>
        public DelegateCommand Custom { get; set; }

        /// <summary>
        /// Gets or sets the Retry.
        /// </summary>
        public DelegateCommand Retry { get; set; }
    }
}
