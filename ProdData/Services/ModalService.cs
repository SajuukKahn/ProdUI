namespace ProdData.Services
{
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using System;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="ModalService" />.
    /// </summary>
    public class ModalService : BindableBase, IModalService
    {
        /// <summary>
        /// Defines the _mediationService.
        /// </summary>
        private readonly IMediationService _mediationService;

        /// <summary>
        /// Defines the _modalFactory.
        /// </summary>
        private readonly IModalFactory _modalFactory;

        /// <summary>
        /// Defines the _modalActive.
        /// </summary>
        private bool _modalActive;

        /// <summary>
        /// Defines the _activeModalData.
        /// </summary>
        private IModalData _activeModalData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalService"/> class.
        /// </summary>
        /// <param name="modalFactory">The modalFactory<see cref="IModalFactory"/>.</param>
        /// <param name="mediationService">The mediationService<see cref="IMediationService"/>.</param>
        public ModalService(IModalFactory modalFactory, IMediationService mediationService)
        {
            _mediationService = mediationService;
            _modalFactory = modalFactory;
            _activeModalData = _modalFactory.CreateModalData();
        }

        /// <summary>
        /// Gets or sets the ActiveModalData.
        /// </summary>
        public IModalData ActiveModalData
        {
            get
            {
                return _activeModalData;
            }

            set
            {
                SetProperty(ref _activeModalData, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ModalActive.
        /// </summary>
        public bool ModalActive
        {
            get
            {
                return _modalActive;
            }

            set
            {
                SetProperty(ref _modalActive, value, () => { _mediationService.PlaybackPaused = true; });
            }
        }

        /// <summary>
        /// The CreateModalData.
        /// </summary>
        /// <returns>The <see cref="IModalData"/>.</returns>
        public IModalData CreateModalData()
        {
            return _modalFactory.CreateModalData();
        }

        /// <summary>
        /// The ShowModal.
        /// </summary>
        /// <param name="modalData">The modalData<see cref="IModalData"/>.</param>
        public void ShowModal(IModalData modalData)
        {
            if (modalData == null)
            {
                if (ActiveModalData == null)
                {
                    return;
                }
            }
            else
            {
                ActiveModalData = modalData;
            }

            if (ActiveModalData.Card != null)
            {
                ActiveModalData.Instructions = "The Step \"" + ActiveModalData.Card.StepTitle + "\" Failed at step #" + (ActiveModalData.Card.CardStepIndex + 1).ToString() + Environment.NewLine + "Select an action below:";
                ActiveModalData.InstructionImage = (BitmapImage)ActiveModalData.Card.StepImage! ?? null;
            }

            if (ActiveModalData.CustomButtonGlyph != null && ActiveModalData.CustomButtonText != null)
            {
                ActiveModalData.CanCustom = true;
            }

            ModalActive = true;
        }

        /// <summary>
        /// The CloseModal.
        /// </summary>
        public void CloseModal()
        {
            ActiveModalData = null;
            ModalActive = false;
        }
    }
}
