namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IControllerService" />.
    /// </summary>
    public interface IControllerService
    {
        /// <summary>
        /// The BeginExecution.
        /// </summary>
        void BeginExecution();

        /// <summary>
        /// The EndExecution.
        /// </summary>
        void EndExecution();
    }
}
