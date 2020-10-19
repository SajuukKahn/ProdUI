namespace ProductionCore.Interfaces.Services
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="IFileService" />.
    /// </summary>
    public interface IFileService
    {
        ObservableCollection<ICard?> LoadProgramSteps();

        ObservableCollection<IProgramData> LoadProgramCollection();
    }
}
