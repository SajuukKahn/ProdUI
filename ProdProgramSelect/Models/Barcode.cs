namespace ProdProgramSelect.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public class Barcode : BindableBase, IBarcode
    {
        /// <summary>
        /// Defines the _barcodeData.
        /// </summary>
        private string? _barcodeData;

        /// <summary>
        /// Defines the _barcodeImage.
        /// </summary>
        private BitmapImage? _barcodeImage;

        /// <summary>
        /// Initializes a new instance of the <see cref="Barcode"/> class.
        /// </summary>
        /// <param name="barcodeData">The barcodeData<see cref="string"/>.</param>
        /// <param name="barcodeImage">The barcodeImage<see cref="BitmapImage"/>.</param>
        public Barcode(string? barcodeData, BitmapImage? barcodeImage)
        {
            _barcodeData = barcodeData;
            _barcodeImage = barcodeImage;
        }

        /// <inheritdoc/>
        public string? BarcodeData
        {
            get
            {
                return _barcodeData ?? string.Empty;
            }

            set
            {
                SetProperty(ref _barcodeData, value);
            }
        }

        /// <inheritdoc/>
        public BitmapImage? BarcodeImage
        {
            get
            {
                return _barcodeImage;
            }

            set
            {
                SetProperty(ref _barcodeImage, value);
            }
        }
    }
}
