namespace ProdData.Models
{
    public class CardStepStatus
    {
        protected readonly uint Status;
        protected readonly string StatusReadout;

        public static readonly CardStepStatus Waiting = new CardStepStatus(0, nameof(Waiting));
        public static readonly CardStepStatus Running = new CardStepStatus(1, nameof(Running));
        public static readonly CardStepStatus Pausing = new CardStepStatus(2, nameof(Pausing));
        public static readonly CardStepStatus Paused = new CardStepStatus(3, nameof(Paused));
        public static readonly CardStepStatus Completed = new CardStepStatus(4, nameof(Completed));

        protected CardStepStatus(uint status, string statusReadout)
        {
            Status = status;
            StatusReadout = statusReadout;
        }

        public override string ToString()
        {
            return StatusReadout;
        }

        public static implicit operator uint(CardStepStatus @enum)
        {
            return @enum.Status;
        }

        public static implicit operator string(CardStepStatus @enum)
        {
            return @enum.StatusReadout;
        }
    }
}
