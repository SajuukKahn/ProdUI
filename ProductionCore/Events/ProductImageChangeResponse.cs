namespace ProductionCore.Events
{
    using System.Windows.Media.Imaging;
    using Prism.Events;

    /// <summary>
    /// Defines the <see cref="ProductImageChangeResponse" />.
    /// </summary>
    public class ProductImageChangeResponse : PubSubEvent<BitmapImage?>
    {
    }
}
