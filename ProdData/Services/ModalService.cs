namespace ProdData.Services
{
    using System;
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
    public class ModalService : BindableBase, IModalService
    {
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
        public ModalService(IModalFactory modalFactory)
        {
            _modalFactory = modalFactory;
            _activeModalData = _modalFactory.CreateModalData();
        }

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public bool ModalActive
        {
            get
            {
                return _modalActive;
            }

            set
            {
                SetProperty(ref _modalActive, value);
            }
        }

        /// <inheritdoc/>
        public IModalData CreateModalData()
        {
            return _modalFactory.CreateModalData();
        }

        /// <inheritdoc/>
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
                ModalActive = true;
                ActiveModalData = modalData;
            }

            if (ActiveModalData.Card != null)
            {
                if (ActiveModalData.Card.StepModalData == null)
                {
                    ActiveModalData.Instructions = "The Step \"" + ActiveModalData.Card.StepTitle + "\" Failed at step #" + (ActiveModalData.Card.CardStepIndex + 1).ToString() + Environment.NewLine + "Select an action below:";
                    ActiveModalData.InstructionImage = (BitmapImage)ActiveModalData.Card.StepImage! ?? null;
                }
            }

            if (ActiveModalData.CustomButtonGlyph != null && ActiveModalData.CustomButtonText != null)
            {
                ActiveModalData.CanCustom = true;
            }
        }

        /// <inheritdoc/>
        public void CloseModal()
        {
            ModalActive = false;
        }
    }
}
