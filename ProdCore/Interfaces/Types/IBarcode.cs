namespace ProdCore.Interfaces
{
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="IBarcode" />.
    /// </summary>
    public interface IBarcode
    {
        /// <summary>
        /// Gets or sets the BarcodeData.
        /// </summary>
        public string? BarcodeData { get; set; }

        /// <summary>
        /// Gets or sets the BarcodeImage.
        /// </summary>
        public BitmapImage? BarcodeImage { get; set; }
    }
}
