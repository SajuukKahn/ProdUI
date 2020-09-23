namespace ProductionCore.Concrete
{
    using System.Windows.Media.Imaging;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="ModalData" />.
    /// </summary>
    public class ModalData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModalData"/> class.
        /// </summary>
        /// <param name="canAbort">The canAbort<see cref="bool"/>.</param>
        /// <param name="canContinue">The canContinue<see cref="bool"/>.</param>
        /// <param name="canRetry">The canRetry<see cref="bool"/>.</param>
        /// <param name="isError">The isError<see cref="bool"/>.</param>
        /// <param name="instructions">The instructions<see cref="string"/>.</param>
        /// <param name="instructionImage">The instructionImage<see cref="BitmapImage"/>.</param>
        /// <param name="customButtonGlyph">The customButtonGlyph<see cref="RadGlyph"/>.</param>
        /// <param name="card">The card<see cref="Card"/>.</param>
        /// <param name="customButtonText">The customButtonText<see cref="string"/>.</param>
        public ModalData(
            bool canAbort = true,
            bool canContinue = false,
            bool canRetry = false,
            bool isError = true,
            string? instructions = null,
            BitmapImage? instructionImage = null,
            RadGlyph? customButtonGlyph = null,
            Card? card = null,
            string? customButtonText = null)
        {
            CustomButtonGlyph = customButtonGlyph;
            CustomButtonText = customButtonText;
            Instructions = instructions ?? string.Empty;
            CanAbort = canAbort;
            CanContinue = canContinue;
            CanRetry = canRetry;
            InstructionImage = instructionImage;
            Card = card;
            IsError = isError;
        }

        /// <summary>
        /// Gets or sets a value indicating whether IsError.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanAbort.
        /// </summary>
        public bool CanAbort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanContinue.
        /// </summary>
        public bool CanContinue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanRetry.
        /// </summary>
        public bool CanRetry { get; set; }

        /// <summary>
        /// Gets or sets the Card.
        /// </summary>
        public Card? Card { get; set; }

        /// <summary>
        /// Gets or sets the CustomButtonGlyph.
        /// </summary>
        public RadGlyph? CustomButtonGlyph { get; set; }

        /// <summary>
        /// Gets or sets the CustomButtonText.
        /// </summary>
        public string? CustomButtonText { get; set; }

        /// <summary>
        /// Gets or sets the InstructionImage.
        /// </summary>
        public BitmapImage? InstructionImage { get; set; }

        /// <summary>
        /// Gets or sets the Instructions.
        /// </summary>
        public string? Instructions { get; set; }
    }
}
