namespace ProductionCore.Interfaces.Services
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="IFileService" />.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// The LoadProgramSteps.
        /// </summary>
        /// <returns>The <see cref="ObservableCollection{ICard}"/>.</returns>
        ObservableCollection<ICard?> LoadProgramSteps();

        /// <summary>
        /// The LoadProgramCollection.
        /// </summary>
        /// <returns>The <see cref="ObservableCollection{IProgramData}"/>.</returns>
        ObservableCollection<IProgramData> LoadProgramCollection();
    }
}
