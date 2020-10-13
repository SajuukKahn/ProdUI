namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IMediationService" />.
    /// </summary>
    public interface IMediationService
    {
        /// <summary>
        /// Gets or sets a value indicating whether PlaybackPaused.
        /// </summary>
        bool PlaybackPaused { get; set; }

        /// <summary>
        /// The BeginExecution.
        /// </summary>
        void BeginExecution();

        /// <summary>
        /// The ExecutionPausedConfirmation.
        /// </summary>
        void ExecutionPausedConfirmation();

        /// <summary>
        /// The AdvanceStep.
        /// </summary>
        void AdvanceStep();

        /// <summary>
        /// The EndExecution.
        /// </summary>
        void EndExecution();
    }
}
