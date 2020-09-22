namespace ProdData.Extensions
{
    using System.ComponentModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Data;
    using Telerik.Windows;
    using Telerik.Windows.Controls;
    using Telerik.Windows.Controls.GridView;

    /// <summary>
    /// Defines the <see cref="GridViewHeaderMenu" />.
    /// </summary>
    public class GridViewHeaderMenu
    {
        /// <summary>
        /// Defines the IsEnabledProperty.
        /// </summary>
        public static readonly DependencyProperty IsEnabledProperty = DependencyProperty.RegisterAttached(
            "IsEnabled",
            typeof(bool),
            typeof(GridViewHeaderMenu),
            new PropertyMetadata(new PropertyChangedCallback(OnIsEnabledPropertyChanged)));

        /// <summary>
        /// Defines the _grid.
        /// </summary>
        private readonly RadGridView _grid;

        /// <summary>
        /// Initializes a new instance of the <see cref="GridViewHeaderMenu"/> class.
        /// </summary>
        /// <param name="grid">The grid<see cref="RadGridView"/>.</param>
        public GridViewHeaderMenu(RadGridView grid)
        {
            _grid = grid;
        }

        /// <summary>
        /// The SetIsEnabled.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject<see cref="DependencyObject"/>.</param>
        /// <param name="enabled">The enabled<see cref="bool"/>.</param>
        public static void SetIsEnabled(DependencyObject dependencyObject, bool enabled)
        {
            dependencyObject.SetValue(IsEnabledProperty, enabled);
        }

        /// <summary>
        /// The GetIsEnabled.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject<see cref="DependencyObject"/>.</param>
        /// <returns>The <see cref="bool"/>.</returns>
        public static bool GetIsEnabled(DependencyObject dependencyObject)
        {
            return (bool)dependencyObject.GetValue(IsEnabledProperty);
        }

        /// <summary>
        /// The OnMenuOpened.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        public void OnMenuOpened(object sender, RoutedEventArgs e)
        {
            RadContextMenu menu = (RadContextMenu)sender;
            GridViewHeaderCell cell = menu.GetClickedElement<GridViewHeaderCell>();

            if (cell != null)
            {
                menu.Items.Clear();

                if (cell.Column.CanFilter())
                {
                    string insertString = (string)(cell.Column.Tag ?? cell.Column.Header);
                    menu.Items.Add(new RadMenuItem
                    {
                        Header = string.Format(@"Sort Ascending by ""{0}""", insertString),
                    });

                    menu.Items.Add(new RadMenuItem
                    {
                        Header = string.Format(@"Sort Descending by ""{0}""", insertString),
                    });

                    menu.Items.Add(new RadMenuItem
                    {
                        Header = string.Format(@"Clear Sorting by ""{0}""", insertString),
                    });

                    menu.Items.Add(new RadMenuItem
                    {
                        Header = string.Format(@"Group by ""{0}""", insertString),
                    });

                    menu.Items.Add(new RadMenuItem
                    {
                        Header = string.Format(@"Ungroup ""{0}""", insertString),
                    });
                }

                RadMenuItem item = new RadMenuItem
                {
                    Header = "Choose Columns:",
                };
                menu.Items.Add(item);

                // create menu items
                foreach (GridViewColumn column in _grid.Columns)
                {
                    RadMenuItem subMenu = new RadMenuItem
                    {
                        Header = column.Tag ?? column.Header,
                        IsCheckable = true,
                        IsChecked = true,
                    };

                    Binding isCheckedBinding = new Binding("IsVisible")
                    {
                        Mode = BindingMode.TwoWay,
                        Source = column,
                    };

                    // bind IsChecked menu item property to IsVisible column property
                    subMenu.SetBinding(RadMenuItem.IsCheckedProperty, isCheckedBinding);
                    item.Items.Add(subMenu);
                }
            }
            else
            {
                menu.IsOpen = false;
            }
        }

        /// <summary>
        /// The OnMenuItemClick.
        /// </summary>
        /// <param name="sender">The sender<see cref="object"/>.</param>
        /// <param name="e">The e<see cref="RoutedEventArgs"/>.</param>
        public void OnMenuItemClick(object sender, RoutedEventArgs e)
        {
            RadContextMenu menu = (RadContextMenu)sender;

            GridViewHeaderCell cell = menu.GetClickedElement<GridViewHeaderCell>();
            RadMenuItem? clickedItem = ((RadRoutedEventArgs)e).OriginalSource as RadMenuItem;
            GridViewColumn column = cell.Column;

            string header = string.Empty;

            if (clickedItem != null)
            {
                if (clickedItem.Parent is RadMenuItem)
                {
                    return;
                }
                else
                {
                    header = (string)clickedItem!.Header ?? string.Empty;
                }
            }

            using (_grid.DeferRefresh())
            {
                ColumnSortDescriptor sd = (from d in _grid.SortDescriptors.OfType<ColumnSortDescriptor>()
                                           where object.Equals(d.Column, column)
                                           select d).FirstOrDefault();

                if (header!.Contains("Sort Ascending"))
                {
                    if (sd != null)
                    {
                        _grid.SortDescriptors.Remove(sd);
                    }

                    ColumnSortDescriptor newDescriptor = new ColumnSortDescriptor
                    {
                        Column = column,
                        SortDirection = ListSortDirection.Ascending,
                    };

                    _grid.SortDescriptors.Add(newDescriptor);
                }
                else if (header.Contains("Sort Descending"))
                {
                    if (sd != null)
                    {
                        _grid.SortDescriptors.Remove(sd);
                    }

                    ColumnSortDescriptor newDescriptor = new ColumnSortDescriptor
                    {
                        Column = column,
                        SortDirection = ListSortDirection.Descending,
                    };

                    _grid.SortDescriptors.Add(newDescriptor);
                }
                else if (header.Contains("Clear Sorting"))
                {
                    if (sd != null)
                    {
                        _grid.SortDescriptors.Remove(sd);
                    }
                }
                else if (header.Contains("Group by"))
                {
                    ColumnGroupDescriptor gd = (from d in _grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
                                                where object.Equals(d.Column, column)
                                                select d).FirstOrDefault();

                    if (gd == null)
                    {
                        ColumnGroupDescriptor newDescriptor = new ColumnGroupDescriptor
                        {
                            Column = column,
                            SortDirection = ListSortDirection.Ascending,
                        };
                        _grid.GroupDescriptors.Add(newDescriptor);
                    }
                }
                else if (header.Contains("Ungroup"))
                {
                    ColumnGroupDescriptor gd = (from d in _grid.GroupDescriptors.OfType<ColumnGroupDescriptor>()
                                                where object.Equals(d.Column, column)
                                                select d).FirstOrDefault();
                    if (gd != null)
                    {
                        _grid.GroupDescriptors.Remove(gd);
                    }
                }
            }
        }

        /// <summary>
        /// The OnIsEnabledPropertyChanged.
        /// </summary>
        /// <param name="dependencyObject">The dependencyObject<see cref="DependencyObject"/>.</param>
        /// <param name="e">The e<see cref="DependencyPropertyChangedEventArgs"/>.</param>
        private static void OnIsEnabledPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
        {
            if (dependencyObject is RadGridView grid)
            {
                if ((bool)e.NewValue)
                {
                    // Create new GridViewHeaderMenu and attach RowLoaded event.
                    GridViewHeaderMenu menu = new GridViewHeaderMenu(grid);
                    menu.Attach();
                }
            }
        }

        /// <summary>
        /// The Attach.
        /// </summary>
        private void Attach()
        {
            if (_grid != null)
            {
                // create menu
                RadContextMenu contextMenu = new RadContextMenu();

                // set menu Theme
                StyleManager.SetTheme(contextMenu, StyleManager.GetTheme(_grid));

                contextMenu.Opened += OnMenuOpened;
                contextMenu.ItemClick += OnMenuItemClick;

                RadContextMenu.SetContextMenu(_grid, contextMenu);
            }
        }
    }
}
