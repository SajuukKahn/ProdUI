namespace ProdProgramSelect.Factories
{
    using System.Windows.Media.Imaging;
    using global::ProdProgramSelect.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="BarcodeFactory" />.
    /// </summary>
    public class BarcodeFactory : IBarcodeFactory
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeFactory"/> class.
        /// </summary>
        public BarcodeFactory()
        {
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="barcodeData">The barcodeData<see cref="string"/>.</param>
        /// <param name="barcodeImage">The barcodeImage<see cref="BitmapImage"/>.</param>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode Create(string? barcodeData, BitmapImage? barcodeImage)
        {
            return new Barcode(barcodeData, barcodeImage);
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode Create()
        {
            return new Barcode(null, null);
        }
    }
}
