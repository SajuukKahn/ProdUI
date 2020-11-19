namespace ProdCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ICardSubStepFactory" />.
    /// </summary>
    public interface ICardSubStepFactory
    {
        /// <summary>
        /// The Create.
        /// </summary>
        /// <returns>The <see cref="ICardSubStep"/>.</returns>
        ICardSubStep Create();

        /// <summary>
        /// The Create.
        /// </summary>
        /// <param name="subStepName">The subStepName<see cref="string"/>.</param>
        /// <param name="subStepData">The subStepData<see cref="string"/>.</param>
        /// <returns>The <see cref="ICardSubStep"/>.</returns>
        ICardSubStep Create(string subStepName, string[] subStepData);
    }
}
