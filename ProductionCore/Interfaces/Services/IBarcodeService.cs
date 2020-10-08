namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IBarcodeService" />.
    /// </summary>
    public interface IBarcodeService
    {
        /// <summary>
        /// The CreateBarcode.
        /// </summary>
        /// <returns>The <see cref="IBarcode"/>.</returns>
        public IBarcode CreateBarcode();
    }
}
