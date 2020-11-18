namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IControllerService" />.
    /// </summary>
    public interface IControllerService
    {
        /// <summary>
        /// Gets or sets a value indicating whether ExecutionPaused.
        /// </summary>
        public bool ExecutionPaused { get; set; }

        /// <summary>
        /// The BeginExecution.
        /// </summary>
        void BeginExecution();

        /// <summary>
        /// The EndExecution.
        /// </summary>
        void EndExecution();

        /// <summary>
        /// The PauseExecution.
        /// </summary>
        void PauseExecution();
    }
}
