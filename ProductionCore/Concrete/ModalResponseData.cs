﻿namespace ProductionCore.Concrete
{
    using global::ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="ModalResponseData" />.
    /// </summary>
    public class ModalResponseData : IModalResponseData
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
