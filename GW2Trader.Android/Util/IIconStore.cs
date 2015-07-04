using Android.App;
using Android.Widget;
using GW2Trader.Model;

namespace GW2Trader.Android.Util
{
    public interface IIconStore
    {
        void SetIcon(Item item, ImageView view, Activity activity);
    }
}