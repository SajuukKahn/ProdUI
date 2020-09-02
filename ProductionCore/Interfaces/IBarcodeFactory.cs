using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ProductionCore.Interfaces
{
    public interface IBarcodeFactory
    {
        IBarcode Create(string? barcodeData, BitmapImage? barcodeImage);
        IBarcode Create();
    }
}
