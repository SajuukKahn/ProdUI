namespace ProdData.Services
{
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ModalService" />.
    /// </summary>
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
        private IModalData? _activeModalData;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalService"/> class.
        /// </summary>
        /// <param name="modalFactory">The modalFactory<see cref="IModalFactory"/>.</param>
        public ModalService(IModalFactory modalFactory)
        {
            _modalFactory = modalFactory;
        }

        /// <summary>
        /// Gets a value indicating whether ModalActive
        /// </summary>
        public bool ModalActive
        {
            get
            {
                return _modalActive;
            }

            private set
            {
                SetProperty(ref _modalActive, value);
            }
        }

        /// <summary>
        /// Gets or sets the ActiveModalData.
        /// </summary>
        public IModalData? ActiveModalData
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
            ActiveModalData = modalData;
            ModalActive = true;
        }
    }
}
