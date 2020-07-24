using System;
using System.Diagnostics;
using System.Windows.Input;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace ProdData.Extensions
{
    public class SelectedFirstItemOnSourceUpdate : Behavior<RadCarousel>
    {
        private object _holdPlace;

        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += (s, e) => DefaultSourceUpdate(e);
            AssociatedObject.IsHitTestVisibleChanged += (s, e) => HoldSelectedItems();
        }

        private void HoldSelectedItems()
        {
            if(AssociatedObject.IsHitTestVisible)
            {
                _holdPlace = AssociatedObject.SelectedItem;
            }
            else
            {
                AssociatedObject.SelectedItem = _holdPlace;
                AssociatedObject.BringDataItemIntoView(AssociatedObject.SelectedItem);
            }
        }

        private void DefaultSourceUpdate(SelectionChangeEventArgs e)
        {
            if (e.AddedItems.Count == 1 && AssociatedObject.Items.Count > 0 && e.AddedItems[0] == AssociatedObject.Items?[0])
            {
                AssociatedObject.BringDataItemIntoView(AssociatedObject.SelectedItem);
            }
        }
    }
}