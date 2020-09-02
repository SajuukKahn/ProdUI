using ProdData.Models;
using ProductionCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Windows.Media.Imaging;

namespace ProdData.Factories
{
    public class BarcodeFactory : IBarcodeFactory
    {
        public IBarcode Create(string? barcodeData, BitmapImage? barcodeImage)
        {
            return new Barcode(barcodeData, barcodeImage);
        }
        public IBarcode Create()
        {
            return new Barcode(null, null);
        }
    }
}
