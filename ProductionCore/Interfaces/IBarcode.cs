using System.Windows.Media.Imaging;

namespace ProductionCore.Interfaces
{
    public interface IBarcode
    {
        public string? BarcodeData { get; set; }
        public BitmapImage? BarcodeImage { get; set; }
    }
}