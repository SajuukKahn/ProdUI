namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IMediationService" />.
    /// </summary>
    public interface IMediationService
    {
        /// <summary>
        /// Gets or sets a value indicating whether PlaybackPaused......
        /// </summary>
        bool PlaybackPaused { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether PauseComplete...
        /// </summary>
        bool PauseComplete { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether BeginExecute.....
        /// </summary>
        bool BeginExecute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether EndExecute....
        /// </summary>
        bool EndExecute { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether ProgramRequestShow..
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
