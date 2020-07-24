using Prism.Events;
using System.Windows.Media.Imaging;

namespace ProdData.Events
{
    public class ProcessDisplayChangeResponse : PubSubEvent<BitmapImage?>
    {
    }
}