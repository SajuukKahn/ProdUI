namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IFileService" />.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// The RetrieveProgram.
        /// </summary>
        /// <param name="program">The program<see cref="IProgramData"/>.</param>
        void RetrieveProgram(IProgramData program);

        /// <summary>
        /// The SaveToJSON.
        /// </summary>
        /// <param name="obj">The obj<see cref="object"/>.</param>
        void SaveToJSON(object obj);

        /// <summary>
        /// The RetrieveProgramCollection.
        /// </summary>
        void RetrieveProgramCollection();
    }
}
