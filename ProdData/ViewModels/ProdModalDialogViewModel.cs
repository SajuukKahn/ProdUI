namespace ProdData.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
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
                playbackService.Play();
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

        /// <inheritdoc/>
        public IModalService ModalService
        {
            get
            {
                return _modalService;
            }
        }

        /// <inheritdoc/>
        public DelegateCommand Abort { get; set; }

        /// <inheritdoc/>
        public DelegateCommand Continue { get; set; }

        /// <inheritdoc/>
        public DelegateCommand Custom { get; set; }

        /// <inheritdoc/>
        public DelegateCommand Retry { get; set; }
    }
}
