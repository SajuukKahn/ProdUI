namespace ProdData.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="ModalData" />.
    /// </summary>
    public class ModalData : BindableBase, IModalData
    {
        /// <summary>
        /// Defines the _isError.
        /// </summary>
        private bool _isError;

        /// <summary>
        /// Defines the _canAbort.
        /// </summary>
        private bool _canAbort;

        /// <summary>
        /// Defines the _canContinue.
        /// </summary>
        private bool _canContinue;

        /// <summary>
        /// Defines the _canRetry.
        /// </summary>
        private bool _canRetry;

        /// <summary>
        /// Defines the _canCustom.
        /// </summary>
        private bool _canCustom;

        /// <summary>
        /// Defines the _card.
        /// </summary>
        private ICard? _card;

        /// <summary>
        /// Defines the _instructions.
        /// </summary>
        private string? _instructions;

        /// <summary>
        /// Defines the _customButtonGlyph.
        /// </summary>
        private RadGlyph? _customButtonGlyph;

        /// <summary>
        /// Defines the _customButtonText.
        /// </summary>
        private string? _customButtonText;

        /// <summary>
        /// Defines the _instructionImage.
        /// </summary>
        private BitmapImage? _instructionImage;

        /// <summary>
        /// Gets or sets a value indicating whether IsError.
        /// </summary>
        public bool IsError
        {
            get
            {
                return _isError;
            }

            set
            {
                SetProperty(ref _isError, value);
            }
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
        /// Gets or sets the Instructions.
        /// </summary>
        public string? Instructions
        {
            get
            {
                return _instructions;
            }

            set
            {
                SetProperty(ref _instructions, value);
            }
        }

        /// <summary>
        /// Gets or sets the InstructionImage.
        /// </summary>
        public BitmapImage? InstructionImage
        {
            get
            {
                return _instructionImage;
            }

            set
            {
                SetProperty(ref _instructionImage, value);
            }
        }

        /// <summary>
        /// Gets or sets the Card.
        /// </summary>
        public ICard? Card
        {
            get
            {
                return _card;
            }

            set
            {
                SetProperty(ref _card, value);
            }
        }

        /// <summary>
        /// Gets or sets the CustomButtonGlyph.
        /// </summary>
        public RadGlyph? CustomButtonGlyph
        {
            get
            {
                return _customButtonGlyph;
            }

            set
            {
                SetProperty(ref _customButtonGlyph, value);
            }
        }

        /// <summary>
        /// Gets or sets the CustomButtonText.
        /// </summary>
        public string? CustomButtonText
        {
            get
            {
                return _customButtonText;
            }

            set
            {
                SetProperty(ref _customButtonText, value);
            }
        }
    }
}
