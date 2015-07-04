using System.Collections.Generic;
using GW2Trader.Desktop.Model;

namespace GW2Trader.Desktop.ViewModel
{
    public interface IItemViewer
    {
        IList<GameItemModel> ShownItems { get; }
    }
}
