using ProdData.Models;
using ProductionCore.Interfaces;
using System.Windows.Media.Imaging;

namespace ProdData.Factories
{
    public class ProgramDataFactory : IProgramDataFactory
    {
        private readonly IBarcodeFactory _barcodeFactory;
        public ProgramDataFactory(IBarcodeFactory barcodeFactory)
        {
            _barcodeFactory = barcodeFactory;
        }

        public IProgramData Create()
        {
            return new ProgramData(_barcodeFactory.Create());
        }

        public IProgramData Create(string? barcodeData, BitmapImage? barcodeImage)
        {
            return new ProgramData(_barcodeFactory.Create(barcodeData, barcodeImage));
        }
    }
}