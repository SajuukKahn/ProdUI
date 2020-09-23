namespace ProductionCore.Interfaces
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the <see cref="IProgramCollection" />.
    /// </summary>
    public interface IProgramCollection
    {
        /// <summary>
        /// Gets or sets the ProgramList.
        /// </summary>
        ObservableCollection<IProgramData>? ProgramList { get; set; }
    }
}
