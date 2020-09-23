namespace ProductionCore.Events
{
    using global::ProductionCore.Interfaces;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ProgramDataRequest" />.
    /// </summary>
    public class ProgramDataRequest : PubSubEvent<IProgramData?>
    {
    }
}
