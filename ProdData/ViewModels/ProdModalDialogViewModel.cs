namespace ProdData.ViewModels
{
    using System;
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="ProdModalDialogViewModel" />.
    /// </summary>
    internal class ProdModalDialogViewModel : BindableBase
    {
        /// <summary>
        /// Defines the _canAbort.
        /// </summary>
        private bool _canAbort;

        /// <summary>
        /// Defines the _canContinue.
        /// </summary>
        private bool _canContinue;

        /// <summary>
        /// Defines the _canCustom.
        /// </summary>
        private bool _canCustom;

        /// <summary>
        /// Defines the _canRetry.
        /// </summary>
        private bool _canRetry;

        /// <summary>
        /// Defines the _customGlyph.
        /// </summary>
        private RadGlyph? _customGlyph;

        /// <summary>
        /// Defines the _customText.
        /// </summary>
        private string? _customText;

        /// <summary>
        /// Defines the _imageSource.
        /// </summary>
        private BitmapImage? _imageSource;

        /// <summary>
        /// Defines the _modalInstructions.
        /// </summary>
        private string? _modalInstructions = "An error was thrown";

        /// <summary>
        /// Defines the _modalOpenRequested.
        /// </summary>
        private bool _modalOpenRequested;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProdModalDialogViewModel"/> class.
        /// </summary>
        public ProdModalDialogViewModel()
        {
            DelegateAbort = new Prism.Commands.DelegateCommand(RaiseAbort).ObservesCanExecute(() => CanAbort);
            DelegateContinue = new Prism.Commands.DelegateCommand(RaiseContinue).ObservesCanExecute(() => CanContinue);
            DelegateRetry = new Prism.Commands.DelegateCommand(RaiseRetry).ObservesCanExecute(() => CanRetry);
            DelegateCustom = new Prism.Commands.DelegateCommand(RaiseCustom).ObservesCanExecute(() => CanCustom);
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanAbort.
        /// </summary>
        public bool CanAbort
        {
            get
            {
                return _canAbort;
            }

            set
            {
                SetProperty(ref _canAbort, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanContinue.
        /// </summary>
        public bool CanContinue
        {
            get
            {
                return _canContinue;
            }

            set
            {
                SetProperty(ref _canContinue, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanCustom.
        /// </summary>
        public bool CanCustom
        {
            get
            {
                return _canCustom;
            }

            set
            {
                SetProperty(ref _canCustom, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether CanRetry.
        /// </summary>
        public bool CanRetry
        {
            get
            {
                return _canRetry;
            }

            set
            {
                SetProperty(ref _canRetry, value);
            }
        }

        /// <summary>
        /// Gets or sets the CustomGlyph.
        /// </summary>
        public RadGlyph? CustomGlyph
        {
            get
            {
                return _customGlyph;
            }

            set
            {
                SetProperty(ref _customGlyph, value);
            }
        }

        /// <summary>
        /// Gets or sets the CustomText.
        /// </summary>
        public string? CustomText
        {
            get
            {
                return _customText;
            }

            set
            {
                SetProperty(ref _customText, value);
            }
        }

        /// <summary>
        /// Gets or sets the DelegateAbort.
        /// </summary>
        public Prism.Commands.DelegateCommand DelegateAbort { get; set; }

        /// <summary>
        /// Gets or sets the DelegateContinue.
        /// </summary>
        public Prism.Commands.DelegateCommand DelegateContinue { get; set; }

        /// <summary>
        /// Gets or sets the DelegateCustom.
        /// </summary>
        public Prism.Commands.DelegateCommand DelegateCustom { get; set; }

        /// <summary>
        /// Gets or sets the DelegateRetry.
        /// </summary>
        public Prism.Commands.DelegateCommand DelegateRetry { get; set; }

        /// <summary>
        /// Gets or sets the ImageSource.
        /// </summary>
        public BitmapImage? ImageSource
        {
            get
            {
                return _imageSource;
            }

            set
            {
                SetProperty(ref _imageSource, value);
            }
        }

        /// <summary>
        /// Gets or sets the ModalInstructions.
        /// </summary>
        public string? ModalInstructions
        {
            get
            {
                return _modalInstructions;
            }

            set
            {
                SetProperty(ref _modalInstructions, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether ModalOpenRequested.
        /// </summary>
        public bool ModalOpenRequested
        {
            get
            {
                return _modalOpenRequested;
            }

            set
            {
                SetProperty(ref _modalOpenRequested, value);
            }
        }

        /// <summary>
        /// The HandleModalRequest.
        /// </summary>
        /// <param name="modalData">The modalData<see cref="IModalData"/>.</param>
        private void HandleModalRequest(IModalData modalData)
        {
            if (modalData == null)
            {
                return;
            }

            ModalOpenRequested = true;
            if (modalData.IsError)
            {
                //// _eventAggregator.GetEvent<ProgramHaltRequest>().Publish();
            }

            if (modalData.Card != null)
            {
                ModalInstructions = "The Step \"" + modalData.Card.StepTitle + "\" Failed at step #" + (modalData.Card.CardStepIndex + 1).ToString()
                    + Environment.NewLine + "Select an action below:";
                ImageSource = (BitmapImage)modalData.Card.StepImage! ?? null;
            }
            else
            {
                ImageSource = modalData.InstructionImage;
                ModalInstructions = modalData.Instructions;
            }

            if (modalData.CustomButtonGlyph != null && modalData.CustomButtonText != null)
            {
                CanCustom = true;
                CustomGlyph = modalData.CustomButtonGlyph;
                CustomGlyph = new RadGlyph() { Glyph = "&#xe50f" };
                CustomText = modalData.CustomButtonText;
            }

            CanAbort = modalData.CanAbort;
            CanRetry = modalData.CanRetry;
            CanContinue = modalData.CanContinue;
        }

        /// <summary>
        /// The RaiseAbort.
        /// </summary>
        private void RaiseAbort()
        {
            //// _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Abort);
            ModalOpenRequested = false;
        }

        /// <summary>
        /// The RaiseContinue.
        /// </summary>
        private void RaiseContinue()
        {
            //// _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Continue);
            ModalOpenRequested = false;
        }

        /// <summary>
        /// The RaiseCustom.
        /// </summary>
        private void RaiseCustom()
        {
            //// _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Custom);
            ModalOpenRequested = false;
        }

        /// <summary>
        /// The RaiseRetry.
        /// </summary>
        private void RaiseRetry()
        {
            //// _eventAggregator.GetEvent<ModalResponse>().Publish(ModalResponseData.Retry);
            ModalOpenRequested = false;
        }
    }
}
