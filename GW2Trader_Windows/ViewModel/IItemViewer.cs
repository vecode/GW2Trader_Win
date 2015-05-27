using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Model;

namespace GW2Trader_Windows.ViewModel
{
    public interface IItemViewer
    {
        IList<GameItemModel> ShownItems { get; }
    }
}
