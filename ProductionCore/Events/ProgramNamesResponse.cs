using Prism.Events;
using ProductionCore.Concrete;
using System.Collections.ObjectModel;

namespace ProductionCore.Events
{
    public class ProgramNamesResponse : PubSubEvent<ObservableCollection<ProgramData>>
    {
    }
}