namespace ProdProgramSelect.Factories
{
    using ProdProgramSelect.Models;
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
    }
}
