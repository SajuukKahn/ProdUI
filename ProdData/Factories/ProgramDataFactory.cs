namespace ProdData.Factories
{
    using System.Windows.Media.Imaging;
    using global::ProdData.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ProgramDataFactory" />.
    /// </summary>
    public class ProgramDataFactory : IProgramDataFactory
    {
        /// <summary>
        /// Defines the _barcodeFactory.
        /// </summary>
        private readonly IBarcodeFactory _barcodeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgramDataFactory"/> class.
        /// </summary>
        /// <param name="barcodeFactory">Resolved registered type for <see cref="IBarcodeFactory"/>.</param>
        public ProgramDataFactory(IBarcodeFactory barcodeFactory)
        {
            _barcodeFactory = barcodeFactory;
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        public IProgramData Create()
        {
            return new ProgramData(_barcodeFactory.Create());
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="barcodeData">The barcodeData<see cref="string"/>.</param>
        /// <param name="barcodeImage">The barcodeImage<see cref="BitmapImage"/>.</param>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        public IProgramData Create(string? barcodeData, BitmapImage? barcodeImage)
        {
            return new ProgramData(_barcodeFactory.Create(barcodeData, barcodeImage));
        }
    }
}
