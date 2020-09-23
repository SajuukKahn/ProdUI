namespace ProductionCore.Events
{
    using global::ProductionCore.Interfaces;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ProgramSelectResponse" />.
    /// </summary>
    public class ProgramSelectResponse : PubSubEvent<IProgramData?>
    {
    }
}
