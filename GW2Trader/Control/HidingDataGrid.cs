using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GW2Trader.Control
{
    /// <summary>
    /// This datagrid hides fields with data annotation [Browsable(false)].
    /// </summary>

    public class HidingDataGrid : DataGrid
    {
        /// Source: 
        /// http://stackoverflow.com/questions/2816929/wpf-toolkit-datagrid-show-fields-even-with-browsable-attribute-set-to-false
        protected override void OnAutoGeneratingColumn(DataGridAutoGeneratingColumnEventArgs e)
        {
            if (((PropertyDescriptor)e.PropertyDescriptor).IsBrowsable == false)
                e.Cancel = true;
        }
    }
}
