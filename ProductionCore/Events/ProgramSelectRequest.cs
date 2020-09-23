namespace ProductionCore.Events
{
    using global::ProductionCore.Interfaces;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ProgramSelectRequest" />.
    /// </summary>
    public class ProgramSelectRequest : PubSubEvent<IProgramData?>
    {
    }
}
