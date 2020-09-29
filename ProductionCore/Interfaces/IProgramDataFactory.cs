namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IProgramDataFactory" />.
    /// </summary>
    public interface IProgramDataFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="IProgramData"/>.</returns>
        IProgramData Create();
    }
}
