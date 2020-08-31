using Prism.Events;
using ProductionCore.Concrete;
using System.Collections.ObjectModel;

namespace ProductionCore.Events
{
    public class ProgramDataResponse : PubSubEvent<ObservableCollection<Card?>>
    {
    }
}