namespace ProductionCore.Events
{
    using System.Collections.ObjectModel;
    using global::ProductionCore.Concrete;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ProgramDataResponse" />.
    /// </summary>
    public class ProgramDataResponse : PubSubEvent<ObservableCollection<Card?>>
    {
    }
}
