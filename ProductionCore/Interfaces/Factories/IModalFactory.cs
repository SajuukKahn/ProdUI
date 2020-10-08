namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IModalFactory" />.
    /// </summary>
    public interface IModalFactory
    {
        /// <summary>
        /// The CreateModalData.
        /// </summary>
        /// <returns>The <see cref="IModalData"/>.</returns>
        IModalData CreateModalData();
    }
}
