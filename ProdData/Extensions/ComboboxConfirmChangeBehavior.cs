using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Interactivity;
using Telerik.Windows.Controls;

namespace ProdData.Extensions
{
    public class ComboboxConfirmChangeBehavior : Behavior<RadMultiColumnComboBox>
    {
        private bool _changePrograms;

        protected override void OnAttached()
        {
            AssociatedObject.SelectionChanged += AssociatedObject_SelectionChanged;
        }

        private void AssociatedObject_SelectionChanged(object sender, SelectionChangeEventArgs e)
        {
            // open dialog box
            // ask for confirmation of change with warning
            // get result
            // set selection to either
            object newSelection = e.AddedItems;
            object oldSelection = e.RemovedItems;
            RadWindow.Confirm(new DialogParameters()
            { 
                DialogStartupLocation = WindowStartupLocation.CenterOwner,
                Content = "Changing Program will end the current program immediately\n\nAre you sure you want to do this?",
                Closed = DialogClosed
            });
            e.Handled = true;
            if (_changePrograms)
            {
                AssociatedObject.SelectedItem = newSelection;
            }
            else
            {
                AssociatedObject.SelectedItem = oldSelection;
            }
        }

        private void DialogClosed(object sender, WindowClosedEventArgs e)
        {
            var result = e.DialogResult;
            _changePrograms = (bool)result;
        }
    }
}
