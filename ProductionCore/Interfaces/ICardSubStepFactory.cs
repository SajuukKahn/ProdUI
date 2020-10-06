namespace ProductionCore.Interfaces
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
    }
}
