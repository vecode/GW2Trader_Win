using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace GW2Trader.Control
{
    /// <summary>
    ///     - this datagrid hides fields with data annotation [Browsable(false)]
    ///     - able to bind to SelectedItems
    /// </summary>
    public class CustomDataGrid : DataGrid
    {
        public CustomDataGrid()
        {
            SelectionChanged += CustomDataGrid_SelectionChanged;
            SelectedItemsList = new ArrayList();
            RowMargin = new Thickness(0);
        }

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public Thickness RowMargin
        {
            get { return (Thickness)GetValue(RowMarginProperty); }
            set { SetValue(RowMarginProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(CustomDataGrid), new PropertyMetadata(null));

        public static readonly DependencyProperty RowMarginProperty =
            DependencyProperty.Register(("RowMargin"), typeof(Thickness), typeof(CustomDataGrid),
                new PropertyMetadata(null));

        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (((PropertyDescriptor)e.PropertyDescriptor).IsBrowsable == false)
                e.Cancel = true;
        }

        void CustomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = this.SelectedItems;
        }

        protected override void OnLoadingRow(DataGridRowEventArgs e)
        {
            e.Row.Margin = RowMargin;
        }
    }
}