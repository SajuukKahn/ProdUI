namespace ProdData.Models
{
    public class CardSubStep
    {
        private string[] _subStepData;

        private string _subStepName;

        public CardSubStep(string subStepName, string[] subStepData)
        {
            SubStepName = subStepName;
            SubStepData = subStepData;
        }

        public string FullSubStepData
        {
            get
            {
                return string.Join(", ", _subStepData);
            }
        }

        public string[] SubStepData
        {
            get
            {
                return _subStepData;
            }
            set
            {
                _subStepData = value;
            }
        }

        public string SubStepName
        {
            get
            {
                return _subStepName;
            }
            set
            {
                _subStepName = value;
            }
        }
    }
}