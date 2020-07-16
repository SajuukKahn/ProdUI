using Prism.Events;
using ProdData.Models;
using System.Collections.ObjectModel;

namespace ProdData.Events
{
    public class ProgramDataResponse : PubSubEvent<ObservableCollection<Card>>
    {
    }
}
