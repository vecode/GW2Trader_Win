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
        /// Source: 
        /// http://stackoverflow.com/questions/2816929/wpf-toolkit-datagrid-show-fields-even-with-browsable-attribute-set-to-false
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (((PropertyDescriptor)e.PropertyDescriptor).IsBrowsable == false)
                e.Cancel = true;
        }

        public CustomDataGrid()
        {
            SelectionChanged += CustomDataGrid_SelectionChanged;
            SelectedItemsList = new ArrayList();
        }

        void CustomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = this.SelectedItems;
        }

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
                DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(CustomDataGrid), new PropertyMetadata(null));
    }
}