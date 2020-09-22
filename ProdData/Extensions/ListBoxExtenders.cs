namespace ProdData.Extensions
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Defines the <see cref="ListBoxExtenders" />.
    /// </summary>
    public class ListBoxExtenders : DependencyObject
    {
        /// <summary>
        /// Defines the AutoScrollToCurrentItemProperty.
        /// </summary>
        public static readonly DependencyProperty AutoScrollToCurrentItemProperty = DependencyProperty.RegisterAttached("AutoScrollToCurrentItem", typeof(bool), typeof(ListBoxExtenders), new UIPropertyMetadata(default(bool), OnAutoScrollToCurrentItemChanged));

        /// <summary>
        /// The GetAutoScrollToCurrentItem.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetAutoScrollToCurrentItem(DependencyObject obj)
        {
            return (bool)obj.GetValue(AutoScrollToCurrentItemProperty);
        }

        /// <summary>
        /// The OnAutoScrollToCurrentItem.
        /// </summary>
        /// <param name="listBox">The listBox<see cref="ListBox"/>.</param>
        /// <param name="index">The index<see cref="int"/>.</param>
        public static void OnAutoScrollToCurrentItem(ListBox listBox, int index)
        {
            if (listBox != null && listBox.Items != null && listBox.Items.Count > index && index >= 0)
            {
                listBox.ScrollIntoView(listBox.Items[index]);
            }
        }

        /// <summary>
        /// The OnAutoScrollToCurrentItemChanged.
        /// </summary>
        /// <param name="s">The s<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        public static void OnAutoScrollToCurrentItemChanged(DependencyObject s, DependencyPropertyChangedEventArgs e)
        {
            var listBox = s as ListBox;
            if (listBox != null)
            {
                var listBoxItems = listBox.Items;
                if (listBoxItems != null)
                {
                    var newValue = (bool)e.NewValue;

                    var autoScrollToCurrentItemWorker = new EventHandler((s1, e2) => OnAutoScrollToCurrentItem(listBox, listBox.Items.CurrentPosition));

                    if (newValue)
                    {
                        listBoxItems.CurrentChanged += autoScrollToCurrentItemWorker;
                    }
                    else
                    {
                        listBoxItems.CurrentChanged -= autoScrollToCurrentItemWorker;
                    }
                }
            }
        }

        /// <summary>
        /// The SetAutoScrollToCurrentItem.
        /// </summary>
        /// <param name="obj">The obj<see cref="DependencyObject"/>.</param>
        /// <param name="value">The value<see cref="bool"/>.</param>
        public static void SetAutoScrollToCurrentItem(DependencyObject obj, bool value)
        {
            obj.SetValue(AutoScrollToCurrentItemProperty, value);
        }
    }
}
