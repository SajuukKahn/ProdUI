namespace ProdData.ViewModels
{
    using Prism.Commands;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using System;

    /// <inheritdoc/>
    public class ProdModalDialogViewModel : BindableBase, IProdModalDialogViewModel
    {
        /// <summary>
        /// Defines the _modalService.
        /// </summary>
        private readonly IModalService _modalService;

        private readonly IPlaybackService _playbackService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdModalDialogViewModel"/> class.
        /// </summary>
        /// <param name="modalService">The modalService<see cref="IModalService"/>.</param>
        /// <param name="playbackService">The playbackService<see cref="IPlaybackService"/>.</param>
        public ProdModalDialogViewModel(IModalService modalService, IPlaybackService playbackService)
        {
            _modalService = modalService;
            _playbackService = playbackService;

            AbortCommand = new DelegateCommand(Abort).ObservesCanExecute(() => ModalService.ActiveModalData!.CanAbort);

            ContinueCommand = new DelegateCommand(Continue).ObservesCanExecute(() => ModalService.ActiveModalData!.CanContinue);

            RetryCommand = new DelegateCommand(Retry).ObservesCanExecute(() => ModalService.ActiveModalData!.CanRetry);

            CustomCommand = new DelegateCommand(Custom).ObservesCanExecute(() => ModalService.ActiveModalData!.CanCustom);
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
        public DelegateCommand AbortCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand ContinueCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand CustomCommand { get; set; }

        /// <inheritdoc/>
        public DelegateCommand RetryCommand { get; set; }

        /// <summary>
        /// The CustomCommand Method.
        /// </summary>
        private void Custom()
        {
            //// Requires some method?
            ModalService.CloseModal();
        }

        /// <summary>
        /// The RetryCommand Method.
        /// </summary>
        private void Retry()
        {
            _playbackService.RetryCard();
            ModalService.CloseModal();
        }

        /// <summary>
        /// The ContinueCommand Method.
        /// </summary>
        private void Continue()
        {
            _playbackService.Play();
            _modalService.CloseModal();
        }

        /// <summary>
        /// The AbortCommand Method.
        /// </summary>
        private void Abort()
        {
            _playbackService.Abort();
            _modalService.CloseModal();
        }
    }
}
