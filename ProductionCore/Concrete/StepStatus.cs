namespace ProductionCore.Concrete
{
    /// <summary>
    /// Defines the <see cref="StepStatus" />.
    /// </summary>
    public class StepStatus
    {
        /// <summary>
        /// Defines the Completed.
        /// </summary>
        public static readonly StepStatus Completed = new StepStatus(4, nameof(Completed));

        /// <summary>
        /// Defines the Paused.
        /// </summary>
        public static readonly StepStatus Paused = new StepStatus(3, nameof(Paused));

        /// <summary>
        /// Defines the Pausing.
        /// </summary>
        public static readonly StepStatus Pausing = new StepStatus(2, nameof(Pausing));

        /// <summary>
        /// Defines the Running.
        /// </summary>
        public static readonly StepStatus Running = new StepStatus(1, nameof(Running));

        /// <summary>
        /// Defines the Waiting.
        /// </summary>
        public static readonly StepStatus Waiting = new StepStatus(0, nameof(Waiting));

        /// <summary>
        /// Defines the Status.
        /// </summary>
        private readonly uint _status;

        /// <summary>
        /// Defines the StatusReadout.
        /// </summary>
        private readonly string _statusReadout;

        /// <summary>
        /// Initializes a new instance of the <see cref="StepStatus"/> class.
        /// </summary>
        /// <param name="status">The status<see cref="uint"/>.</param>
        /// <param name="statusReadout">The statusReadout<see cref="string"/>.</param>
        protected StepStatus(uint status, string statusReadout)
        {
            _status = status;
            _statusReadout = statusReadout;
        }

        public static implicit operator string(StepStatus @enum)
        {
            return @enum._statusReadout;
        }

        public static implicit operator uint(StepStatus @enum)
        {
            return @enum._status;
        }

        /// <summary>
        /// The ToString.
        /// </summary>
        /// <returns>The <see cref="string"/>.</returns>
        public override string ToString()
        {
            return _statusReadout;
        }
    }
}
