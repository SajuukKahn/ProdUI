namespace ProductionCore.Concrete
{
    /// <summary>
    /// Defines the <see cref="CardSubStep" />.
    /// </summary>
    public class CardSubStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardSubStep"/> class.
        /// </summary>
        /// <param name="subStepName">The subStepName<see cref="string"/>.</param>
        /// <param name="subStepData">The subStepData<see cref="string"/>.</param>
        public CardSubStep(string subStepName, string[] subStepData)
        {
            SubStepName = subStepName;
            SubStepData = subStepData;
        }

        /// <summary>
        /// Gets the FullSubStepData.
        /// </summary>
        public string FullSubStepData
        {
            get
            {
                return string.Join(", ", SubStepData);
            }
        }

        /// <summary>
        /// Gets or sets the SubStepData.
        /// </summary>
        public string[] SubStepData { get; set; }

        /// <summary>
        /// Gets or sets the SubStepName.
        /// </summary>
        public string SubStepName { get; set; }
    }
}
