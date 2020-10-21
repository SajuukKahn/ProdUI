namespace ProductionCore.Interfaces
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the <see cref="IMediationService" />.
    /// </summary>
    public interface IMediationService : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestShow....
        /// </summary>
        bool ProgramRequestShow { get; set; }

        /// <summary>
        /// Gets or sets the CurrentProgram.
        /// </summary>
        IProgramData? CurrentProgram { get; set; }

        /// <summary>
        /// The SaveProgram.
        /// </summary>
        /// <param name="programSuccessful">The programSuccessful<see cref="bool"/>.</param>
        /// <param name="cycleTime">The cycleTime<see cref="IChronometer"/>.</param>
        void SaveProgram(bool programSuccessful, IChronometer? cycleTime);
    }
}
