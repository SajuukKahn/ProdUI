namespace ProductionCore.Concrete
{
    public class StepStatus
    {
        public static readonly StepStatus Completed = new StepStatus(4, nameof(Completed));

        public static readonly StepStatus Paused = new StepStatus(3, nameof(Paused));

        public static readonly StepStatus Pausing = new StepStatus(2, nameof(Pausing));

        public static readonly StepStatus Running = new StepStatus(1, nameof(Running));

        public static readonly StepStatus Waiting = new StepStatus(0, nameof(Waiting));

        protected readonly uint Status;

        protected readonly string StatusReadout;

        //TODO This class is a replacement of an Enum type class... is this an acceptable way to accomplish named 'states' or would simply using a string be a more appropriate method?
        protected StepStatus(uint status, string statusReadout)
        {
            Status = status;
            StatusReadout = statusReadout;
        }

        public static implicit operator string(StepStatus @enum)
        {
            return @enum.StatusReadout;
        }

        public static implicit operator uint(StepStatus @enum)
        {
            return @enum.Status;
        }

        public override string ToString()
        {
            return StatusReadout;
        }
    }
}