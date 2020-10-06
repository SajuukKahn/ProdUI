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
        string[]? SubStepData { get; set; }

        /// <summary>
        /// Gets or sets the SubStepName.
        /// </summary>
        string? SubStepName { get; set; }
    }
}
