using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace GW2Trader.Desktop.Control
{
    public class MultiSelectListBox : ListBox
    {
        public MultiSelectListBox()
        {
            SelectionChanged += MultiSelectListBox_SelectionChanged;
            SelectedItemsList = new ArrayList();
        }

        public IList SelectedItemsList
        {
            get { return (IList)GetValue(SelectedItemsListProperty); }
            set { SetValue(SelectedItemsListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemsListProperty =
        DependencyProperty.Register("SelectedItemsList", typeof(IList), typeof(MultiSelectListBox), new PropertyMetadata(null));

        void MultiSelectListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItemsList = this.SelectedItems;
        }
    }
}
