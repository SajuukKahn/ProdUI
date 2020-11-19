namespace ProdProgramSelect.Factories
{
    using System.Windows.Media.Imaging;
    using ProdProgramSelect.Models;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public class BarcodeFactory : IBarcodeFactory
    {
        /// <inheritdoc/>
        public IBarcode Create(string? barcodeData, BitmapImage? barcodeImage)
        {
            return new Barcode(barcodeData, barcodeImage);
        }

        /// <inheritdoc/>
        public IBarcode Create()
        {
            return new Barcode(null, null);
        }
    }
}
