namespace ProdCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IChronometerFactory" />.
    /// </summary>
    public interface IChronometerFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IChronometer"/>.</returns>
        IChronometer Create();
    }
}
