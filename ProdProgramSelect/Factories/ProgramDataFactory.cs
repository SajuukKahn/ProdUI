namespace ProdProgramSelect.Factories
{
    using ProdProgramSelect.Models;
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
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

        /// <inheritdoc/>
        public IProgramData Create()
        {
            return new ProgramData(_barcodeFactory.Create());
        }
    }
}
