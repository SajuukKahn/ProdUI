namespace ProdData.Models
{
    public class StepStatus
    {
        protected readonly uint Status;
        protected readonly string StatusReadout;

        public static readonly StepStatus Waiting = new StepStatus(0, nameof(Waiting));
        public static readonly StepStatus Running = new StepStatus(1, nameof(Running));
        public static readonly StepStatus Pausing = new StepStatus(2, nameof(Pausing));
        public static readonly StepStatus Paused = new StepStatus(3, nameof(Paused));
        public static readonly StepStatus Completed = new StepStatus(4, nameof(Completed));

        protected StepStatus(uint status, string statusReadout)
        {
            Status = status;
            StatusReadout = statusReadout;
        }

        public override string ToString()
        {
            return StatusReadout;
        }

        public static implicit operator uint(StepStatus @enum)
        {
            return @enum.Status;
        }

        public static implicit operator string(StepStatus @enum)
        {
            return @enum.StatusReadout;
        }
    }
}
