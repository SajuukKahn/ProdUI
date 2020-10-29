namespace ProductionCore.Interfaces
{
    using System;

    /// <summary>
    /// Defines the <see cref="IChronometer" />.
    /// </summary>
    public interface IChronometer
    {
        /// <summary>
        /// Gets or sets the ElapsedTime.
        /// </summary>
        string? ElapsedTime { get; set; }

        /// <summary>
        /// Gets or sets the TimeSpan.
        /// </summary>
        TimeSpan TimeSpan { get; set; }

        /// <summary>
        /// Gets or sets the StartTime.
        /// </summary>
        DateTime? StartTime { get; set; }

        /// <summary>
        /// The Pause.
        /// </summary>
        void Pause();

        /// <summary>
        /// The Reset.
        /// </summary>
        void Reset();

        /// <summary>
        /// The Start.
        /// </summary>
        void Start();
    }
}
