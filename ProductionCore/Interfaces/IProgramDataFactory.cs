namespace ProductionCore.Interfaces
{
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Defines the <see cref="IProgramDataFactory" />.
    /// </summary>
    public interface IProgramDataFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="barcodeData">The barcodeData<see cref="string"/>.</param>
        /// <param name="barcodeImage">The barcodeImage<see cref="BitmapImage"/>.</param>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        IProgramData Create(string? barcodeData, BitmapImage? barcodeImage);

        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        IProgramData Create();
    }
}
