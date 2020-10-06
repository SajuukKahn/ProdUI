namespace ProdData.Factories
{
    using global::ProdData.Models;
    using ProductionCore.Interfaces;

    /// <summary>
    /// Defines the <see cref="CardSubStepFactory" />.
    /// </summary>
    public class CardSubStepFactory : ICardSubStepFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="ICardSubStep"/>.</returns>
        public ICardSubStep Create()
        {
            return new CardSubStep();
        }

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="subStepName">The subStepName<see cref="string"/>.</param>
        /// <param name="subStepData">The subStepData<see cref="string[]"/>.</param>
        /// <returns>The <see cref="ICardSubStep"/>.</returns>
        public ICardSubStep Create(string subStepName, string[] subStepData)
        {
            return new CardSubStep(subStepName, subStepData);
        }
    }
}
