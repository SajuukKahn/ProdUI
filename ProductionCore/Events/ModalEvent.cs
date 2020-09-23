namespace ProductionCore.Events
{
    using global::ProductionCore.Concrete;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ModalEvent" />.
    /// </summary>
    public class ModalEvent : PubSubEvent<ModalData>
    {
    }
}
