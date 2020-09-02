using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media.Imaging;

namespace ProductionCore.Interfaces
{
    public interface IProgramDataFactory
    {
        IProgramData Create(string? barcodeData, BitmapImage? barcodeImage);
        IProgramData Create();
    }
}
