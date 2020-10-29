namespace ProdProgramSelect.Models
{
    using System.Windows.Media.Imaging;
    using Prism.Mvvm;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="Barcode" />.
    /// </summary>
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

        /// <summary>
        /// Gets or sets the BarcodeData.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the BarcodeImage.
        /// </summary>
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
