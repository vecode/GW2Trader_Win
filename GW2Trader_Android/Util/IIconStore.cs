using Android.Graphics;
using GW2Trader.Model;

namespace GW2Trader_Android.Util
{
    public interface IIconStore
    {
        void AddIconForItem(Item item, Bitmap icon);
        Bitmap GetIcon(Item item);
        bool HasIconForItem(Item item);
        void DownloadIcon(Item item);
    }
}