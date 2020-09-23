namespace ProductionCore.Events
{
    using global::ProductionCore.Concrete;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ModalResponse" />.
    /// </summary>
    public class ModalResponse : PubSubEvent<ModalResponseData>
    {
    }
}
