using ProductionCore.Concrete;
using System;
using System.Windows;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace ProdData.Extensions
{
    public class SelectedIndexFromSelectedItem : DependencyObject
    {
        public static readonly DependencyProperty SelectedIndexAttached = DependencyProperty.RegisterAttached("SelectedIndex", typeof(int), typeof(SelectedIndexFromSelectedItem), new PropertyMetadata(0, SelectedIndexAttachedChanged));

        public static int GetSelectedIndex(DependencyObject d)
        {
            RadCarousel dpo = (RadCarousel)d;
            Card card = (Card)dpo.CurrentItem;
            var coll = dpo.Items;
            return coll.IndexOf(card);
        }

        public static void SetSelectedIndex(DependencyObject d, int value)
        {
            d.SetValue(SelectedIndexAttached, value);
        }

        private static void SelectedIndexAttachedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            int newLocation = (int)e.NewValue;
            int oldLocation = (int)e.OldValue;
            if (!d.Equals(null))
            {
                RadCarousel radCarousel = (RadCarousel)d;
                Application.Current.Dispatcher.BeginInvoke(new Action(() => { radCarousel!.SelectedItem = radCarousel.Items[newLocation]; }), DispatcherPriority.Render);
                if (newLocation == 0)
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(() => { radCarousel!.BringDataItemIntoView(radCarousel.SelectedItem); }), DispatcherPriority.Render);
                }
            }
        }
    }
}