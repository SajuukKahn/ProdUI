namespace ProdData.Models
{
    using ProductionCore.Interfaces;

    /// <inheritdoc/>
    public class CardSubStep : ICardSubStep
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CardSubStep"/> class.
        /// </summary>
        public CardSubStep()
        {
        }

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

        /// <inheritdoc/>
        public string FullSubStepData
        {
            get
            {
                if (SubStepData != null)
                {
                    return string.Join(", ", SubStepData);
                }

                return string.Empty;
            }
        }

        /// <inheritdoc/>
        public string[]? SubStepData { get; set; }

        /// <inheritdoc/>
        public string? SubStepName { get; set; }
    }
}
