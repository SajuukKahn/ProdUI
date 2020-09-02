using Prism.Mvvm;
using ProductionCore.Interfaces;
using System.Windows.Media.Imaging;

namespace ProdData.Models
{
    public class Barcode : BindableBase, IBarcode
    {
        private string? _barcodeData;
        private BitmapImage? _barcodeImage;

        public Barcode(string? barcodeData, BitmapImage? barcodeImage)
        {
            _barcodeData = barcodeData;
            _barcodeImage = barcodeImage;
        }

        public string? BarcodeData
        {
            get
            {
                return _barcodeData ?? "";
            }
            set
            {
                SetProperty(ref _barcodeData, value);
            }
        }

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