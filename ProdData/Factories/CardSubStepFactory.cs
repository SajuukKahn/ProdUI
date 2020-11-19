namespace ProdData.Factories
{
    using ProdData.Models;
    using ProdCore.Interfaces;

    /// <inheritdoc/>
    public class CardSubStepFactory : ICardSubStepFactory
    {
        /// <inheritdoc/>
        public ICardSubStep Create()
        {
            return new CardSubStep();
        }

        /// <inheritdoc/>
        public ICardSubStep Create(string subStepName, string[] subStepData)
        {
            return new CardSubStep(subStepName, subStepData);
        }
    }
}
