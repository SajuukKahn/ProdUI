namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IControllerService" />.
    /// </summary>
    public interface IControllerService
    {
        /// <summary>
        /// The AcceptPause.
        /// </summary>
        void AcceptPause();

        /// <summary>
        /// The AcceptPlay.
        /// </summary>
        void AcceptPlay();

        /// <summary>
        /// The SendPause.
        /// </summary>
        void SendPause();

        /// <summary>
        /// The SendPlay.
        /// </summary>
        void SendPlay();

        /// <summary>
        /// The SendAdvance.
        /// </summary>
        void SendAdvance();
    }
}
