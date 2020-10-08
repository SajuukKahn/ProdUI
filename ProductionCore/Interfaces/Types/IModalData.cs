namespace ProductionCore.Interfaces
{
    using System.Windows.Media.Imaging;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="IModalData" />.
    /// </summary>
    public interface IModalData
    {
        /// <summary>
        /// Gets or sets a value indicating whether IsError...
        /// </summary>
        bool IsError { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanAbort...
        /// </summary>
        bool CanAbort { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanContinue...
        /// </summary>
        bool CanContinue { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanRetry...
        /// </summary>
        bool CanRetry { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether CanCustom.
        /// </summary>
        bool CanCustom { get; set; }

        /// <summary>
        /// Gets or sets the Card.
        /// </summary>
        ICard? Card { get; set; }

        /// <summary>
        /// Gets or sets the CustomButtonGlyph.
        /// </summary>
        RadGlyph? CustomButtonGlyph { get; set; }

        /// <summary>
        /// Gets or sets the CustomButtonText.
        /// </summary>
        string? CustomButtonText { get; set; }

        /// <summary>
        /// Gets or sets the InstructionImage.
        /// </summary>
        BitmapImage? InstructionImage { get; set; }

        /// <summary>
        /// Gets or sets the Instructions.
        /// </summary>
        string? Instructions { get; set; }
    }
}
