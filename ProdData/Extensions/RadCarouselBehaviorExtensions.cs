using Microsoft.Xaml.Behaviors;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace ProdData.Extensions
{
    public class RadCarouselBehaviorExtensions : Behavior<RadCarousel>
    {
        protected override void OnAttached()
        {
            AssociatedObject.IsHitTestVisibleChanged += (s, e) => SnapToCurrentItem(s, e);
            AssociatedObject.PropertyChanged += (s, e) => PropChangedEventHandler(s, e);
            AssociatedObject.PreviewMouseWheel += (s, e) => CustomScrollWheelBehavior(s, e);
        }

        private void CustomScrollWheelBehavior(object s, MouseWheelEventArgs e)
        {
            e.Handled = true;
        }

        private void PropChangedEventHandler(object s, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(RadCarousel.CurrentItem))
            {
                RadCarousel radCarousel = s as RadCarousel;
                Dispatcher.BeginInvoke(new Action(() => { radCarousel.FindCarouselPanel().BringDataItemIntoView(radCarousel.CurrentItem); }), DispatcherPriority.Render);
            }
        }

        private void SnapToCurrentItem(object s, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false))
            {
                RadCarousel radCarousel = s as RadCarousel;
                Dispatcher.BeginInvoke(new Action(() => { radCarousel.FindCarouselPanel().BringDataItemIntoView(radCarousel.CurrentItem); }), DispatcherPriority.Render);
            }
        }

    }
}