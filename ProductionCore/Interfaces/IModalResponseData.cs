namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="IModalResponseData" />.
    /// </summary>
    public interface IModalResponseData
    {
        /// <summary>
        /// Defines the Abort.
        /// </summary>
        public static readonly string Abort = "Abort";

        /// <summary>
        /// Defines the Continue.
        /// </summary>
        public static readonly string Continue = "Continue";

        /// <summary>
        /// Defines the Retry.
        /// </summary>
        public static readonly string Retry = "Retry";

        /// <summary>
        /// Gets or sets the Custom.
        /// </summary>
        public string? Custom { get; set; }
    }
}
