namespace ProductionCore.Interfaces
{
    /// <summary>
    /// Defines the <see cref="ICardSubStep" />.
    /// </summary>
    public interface ICardSubStep
    {
        /// <summary>
        /// Gets the FullSubStepData.
        /// </summary>
        string FullSubStepData { get; }

        /// <summary>
        /// Gets or sets the SubStepData.
        /// </summary>
        #pragma warning disable SA1011 // ClosingSquareBracketsMustBeSpacedCorrectly
        string[]? SubStepData { get; set; }

        /// <summary>
        /// Gets or sets the SubStepName.
        /// </summary>
        string? SubStepName { get; set; }
    }
}
