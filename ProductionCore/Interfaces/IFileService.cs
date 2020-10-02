namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IFileService" />.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// The SaveToJSON.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        void SaveToJSON(IProgramData program);
    }
}
