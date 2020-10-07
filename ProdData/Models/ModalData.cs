namespace ProdData.Models
{
    using System.Windows.Media.Imaging;
    using ProductionCore.Interfaces;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="ModalData" />.
    /// </summary>
    public class ModalData : IModalData
    {
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
        public ICard? Card { get; set; }

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
