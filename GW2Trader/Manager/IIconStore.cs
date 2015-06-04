using System.Collections.Generic;
using System.Threading;
using DataLayer.Model;

namespace GW2Trader.Manager
{
    // TODO move to respective GUI project
    public interface IIconStore
    {
        string GetIconPath(Item item);
        void DownloadMissingIcons(List<Item> items, CancellationToken cancellationToken);
    }
}
