namespace ProdCards.Models
{
    public class ProdCardSubStepModel
    {
        public ProdCardSubStepModel(string subStepName, string[] subStepData)
        {
            SubStepName = subStepName;
            SubStepData = subStepData;
        }

        private string _subStepName;
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

        public string FullSubStepData
        {
            get
            {
                return string.Join(", ", _subStepData);
            }
        }

        private string[] _subStepData;
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
    }
}
