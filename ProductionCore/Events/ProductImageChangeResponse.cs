using Prism.Events;
using System.Windows.Media.Imaging;

namespace ProductionCore.Events
{
    public class ProductImageChangeResponse : PubSubEvent<BitmapImage?>
    {
    }
}