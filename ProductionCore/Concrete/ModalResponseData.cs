namespace ProductionCore.Concrete
{
    public class ModalResponseData
    {
        public static readonly ModalResponseData Abort = new ModalResponseData(4, nameof(Abort));

        public static readonly ModalResponseData Continue = new ModalResponseData(2, nameof(Continue));
        public static readonly ModalResponseData Custom = new ModalResponseData(1, nameof(Custom));
        public static readonly ModalResponseData Retry = new ModalResponseData(3, nameof(Retry));
        protected readonly uint Status;

        protected readonly string StatusReadout;

        protected ModalResponseData(uint status, string statusReadout)
        {
            Status = status;
            StatusReadout = statusReadout;
        }

        public static implicit operator string(ModalResponseData @enum)
        {
            return @enum.StatusReadout;
        }

        public static implicit operator uint(ModalResponseData @enum)
        {
            return @enum.Status;
        }

        public override string ToString()
        {
            return StatusReadout;
        }
    }
}