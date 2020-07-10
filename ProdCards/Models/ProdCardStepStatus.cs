namespace ProdCards.Models
{
    public class ProdCardStepStatus
    {
        protected readonly uint Status;
        protected readonly string StatusReadout;

        public static readonly ProdCardStepStatus Waiting = new ProdCardStepStatus(0, nameof(Waiting));
        public static readonly ProdCardStepStatus Running = new ProdCardStepStatus(1, nameof(Running));
        public static readonly ProdCardStepStatus Pausing = new ProdCardStepStatus(2, nameof(Pausing));
        public static readonly ProdCardStepStatus Paused = new ProdCardStepStatus(3, nameof(Paused));
        public static readonly ProdCardStepStatus Completed = new ProdCardStepStatus(4, nameof(Completed));

        protected ProdCardStepStatus(uint status, string statusReadout)
        {
            Status = status;
            StatusReadout = statusReadout;
        }

        public override string ToString()
        {
            return StatusReadout;
        }

        public static implicit operator uint(ProdCardStepStatus @enum)
        {
            return @enum.Status;
        }

        public static implicit operator string(ProdCardStepStatus @enum)
        {
            return @enum.StatusReadout;
        }
    }
}
