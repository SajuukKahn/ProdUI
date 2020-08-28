namespace ProductionCore.Concrete
{
    public class CardSubStep
    {
        public CardSubStep(string subStepName, string[] subStepData)
        {
            SubStepName = subStepName;
            SubStepData = subStepData;
        }

        public string FullSubStepData
        {
            get
            {
                return string.Join(", ", SubStepData);
            }
        }

        public string[] SubStepData { get; set; }

        public string SubStepName { get; set; }
    }
}