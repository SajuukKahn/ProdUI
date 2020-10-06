namespace ProdProgramSelect.Services
{
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="BarcodeService" />.
    /// </summary>
    public class BarcodeService : IBarcodeService
    {
        /// <summary>
        /// Defines the _barcodeFactory.
        /// </summary>
        private readonly IBarcodeFactory _barcodeFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeService"/> class.
        /// </summary>
        /// <param name="barcodeFactory">The barcodeFactory<see cref="IBarcodeFactory"/>.</param>
        public BarcodeService(IBarcodeFactory barcodeFactory)
        {
            _barcodeFactory = barcodeFactory;
        }

        /// <summary>
        /// The CreateBarcode.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode CreateBarcode()
        {
            return _barcodeFactory.Create();
        }
    }
}
