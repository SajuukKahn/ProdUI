namespace ProdData.Extensions
{
    using System;
    using System.Windows;
    using System.Windows.Threading;
    using ProdCore.Interfaces;
    using Telerik.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="SelectedIndexFromSelectedItem" />.
    /// </summary>
    public class SelectedIndexFromSelectedItem : DependencyObject
    {
        /// <summary>
        /// Defines the SelectedIndexAttached.
        /// </summary>
        public static readonly DependencyProperty SelectedIndexAttached = DependencyProperty.RegisterAttached("SelectedIndex", typeof(int), typeof(SelectedIndexFromSelectedItem), new PropertyMetadata(0, SelectedIndexAttachedChanged));

        /// <summary>
        /// The GetSelectedIndex.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="int"/>.</returns>
        public static int GetSelectedIndex(DependencyObject d)
        {
            RadCarousel dpo = (RadCarousel)d;
            ICard card = (ICard)dpo.CurrentItem;
            var coll = dpo.Items;
            return coll.IndexOf(card);
        }

        /// <summary>
        /// The SetSelectedIndex.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="int"/>.</param>
        public static void SetSelectedIndex(DependencyObject d, int value)
        {
            d.SetValue(SelectedIndexAttached, value);
        }

        /// <summary>
        /// The SelectedIndexAttachedChanged.
        /// </summary>
        /// <param name="d">The d<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void SelectedIndexAttachedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int newLocation = (int)e.NewValue;
            int oldLocation = (int)e.OldValue;
            if (!d.Equals(null))
            {
                RadCarousel radCarousel = (RadCarousel)d;
                Application.Current.Dispatcher.BeginInvoke(
                    new Action(() =>
                    {
                        radCarousel!.CurrentItem = radCarousel.Items[newLocation];
                        radCarousel!.SelectedItem = radCarousel!.CurrentItem;
                    }), DispatcherPriority.Render);
                if (newLocation == 0)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => { radCarousel!.BringDataItemIntoView(radCarousel.CurrentItem); }), DispatcherPriority.Render);
                }
            }
        }
    }
}
