using Prism.Events;
using ProdData.Extensions;
using ProdData.Models;

namespace ProdData.Events
{
    public class ProgramDataResponse : PubSubEvent<IndexedObservableCollection<Card>>
    {
    }
}