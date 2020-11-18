namespace ProdData.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Threading;
    using Microsoft.Xaml.Behaviors;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="RadCarouselBehaviorExtensions" />.
    /// </summary>
    public class RadCarouselBehaviorExtensions : Behavior<RadCarousel>
    {
        /// <inheritdoc/>
        protected override void OnAttached()
        {
            AssociatedObject.IsHitTestVisibleChanged += (s, e) => SnapToCurrentItem(s, e);
            AssociatedObject.PropertyChanged += (s, e) => PropChangedEventHandler(s, e);
            AssociatedObject.PreviewMouseWheel += (s, e) => { e.Handled = true; };
            AssociatedObject.PreviewKeyDown += (s, e) => { e.Handled = true; };
            AssociatedObject.PreviewKeyUp += (s, e) => { e.Handled = true; };
            AssociatedObject.PreviewGotKeyboardFocus += (s, e) => { e.Handled = true; };
        }

        /// <summary>
        /// The PropChangedEventHandler.
        /// </summary>
        /// <param name="s">The s<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="PropertyChangedEventArgs"/>.</param>
        private void PropChangedEventHandler(object s, PropertyChangedEventArgs e)
        {
            if (!s.Equals(null))
            {
                RadCarousel radCarousel = (RadCarousel)s;
                if (radCarousel.SelectedItem == null && e.PropertyName == nameof(RadCarousel.CurrentItem))
                {
                    Dispatcher.BeginInvoke(new Action(() => { radCarousel.FindCarouselPanel().BringDataItemIntoView(radCarousel.CurrentItem); }), DispatcherPriority.Render);
                }
            }
        }

        /// <summary>
        /// The SnapToCurrentItem.
        /// </summary>
        /// <param name="s">The s<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private void SnapToCurrentItem(object s, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue.Equals(false) && !s.Equals(null))
            {
                RadCarousel radCarousel = (RadCarousel)s;
                Dispatcher.BeginInvoke(new Action(() => { radCarousel.FindCarouselPanel().BringDataItemIntoView(radCarousel.CurrentItem); }), DispatcherPriority.Render);
            }
        }
    }
}
