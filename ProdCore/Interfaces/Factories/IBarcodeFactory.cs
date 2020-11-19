namespace ProdCore.Interfaces
{
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="IBarcodeFactory" />.
    /// </summary>
    public interface IBarcodeFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="barcodeData">The barcodeData<see cref="string"/>.</param>
        /// <param name="barcodeImage">The barcodeImage<see cref="BitmapImage"/>.</param>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        IBarcode Create(string? barcodeData, BitmapImage? barcodeImage);

        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        IBarcode Create();
    }
}
