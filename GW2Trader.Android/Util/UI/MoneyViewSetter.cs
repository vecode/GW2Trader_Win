using Android.Widget;
using GW2Trader.Util;

namespace GW2Trader.Android.Util.UI
{
    public class MoneyViewSetter
    {
        public static void SetMoneyView(LinearLayout layout, int value)
        {
            var goldShare = value < 0 ? "-" : string.Empty;
            goldShare += MoneyHelper.ExtractGoldShare(value).ToString();
            layout.FindViewById<TextView>(Resource.Id.Gold).Text = goldShare;

            layout.FindViewById<TextView>(Resource.Id.Silver).Text = MoneyHelper.ExtractSilverShare(value).ToString();
            layout.FindViewById<TextView>(Resource.Id.Copper).Text = MoneyHelper.ExtractCopperShare(value).ToString();
        }
    }
}