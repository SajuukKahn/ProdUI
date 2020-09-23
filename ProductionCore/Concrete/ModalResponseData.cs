namespace ProductionCore.Concrete
{
    /// <summary>
    /// Defines the <see cref="ModalResponseData" />.
    /// </summary>
    public class ModalResponseData
    {
        /// <summary>
        /// Defines the Abort.
        /// </summary>
        public static readonly ModalResponseData Abort = new ModalResponseData(4, nameof(Abort));

        /// <summary>
        /// Defines the Continue.
        /// </summary>
        public static readonly ModalResponseData Continue = new ModalResponseData(2, nameof(Continue));

        /// <summary>
        /// Defines the Custom.
        /// </summary>
        public static readonly ModalResponseData Custom = new ModalResponseData(1, nameof(Custom));

        /// <summary>
        /// Defines the Retry.
        /// </summary>
        public static readonly ModalResponseData Retry = new ModalResponseData(3, nameof(Retry));

        /// <summary>
        /// Defines the Status.
        /// </summary>
        private readonly uint _status;

        /// <summary>
        /// Defines the StatusReadout.
        /// </summary>
        private readonly string _statusReadout;

        /// <summary>
        /// Initializes a new instance of the <see cref="ModalResponseData"/> class.
        /// </summary>
        /// <param name="status">The status<see cref="uint"/>.</param>
        /// <param name="statusReadout">The statusReadout<see cref="string"/>.</param>
        protected ModalResponseData(uint status, string statusReadout)
        {
            _status = status;
            _statusReadout = statusReadout;
        }

        public static implicit operator string(ModalResponseData @enum)
        {
            return @enum._statusReadout;
        }

        public static implicit operator uint(ModalResponseData @enum)
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
