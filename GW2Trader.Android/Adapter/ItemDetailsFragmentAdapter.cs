using System.Collections.Generic;
using Android.Support.V4.App;
using GW2Trader.Android.Fragments;
using GW2Trader.Model;
using Java.Lang;
using Fragment = Android.Support.V4.App.Fragment;

namespace GW2Trader.Android.Adapter
{
    public class ItemDetailsFragmentAdapter : FragmentStatePagerAdapter
    {
        public override int Count => 3;
        private readonly Item _item;
        private readonly Dictionary<int, Fragment> _fragmentReference = new Dictionary<int, Fragment>();

        public ItemDetailsFragmentAdapter(FragmentManager fm, Item item)
            : base(fm)
        {
            _item = item;
        }

        public override Fragment GetItem(int position)
        {
            Fragment fragment;
            switch (position)
            {
                case 0:
                    fragment = new ItemDetailsFragment(_item);
                    break;
                case 1:
                    fragment = new PriceListingFragment(_item.BuyOrders, "Buy", "Demand");
                    break;
                case 2:
                    fragment = new PriceListingFragment(_item.SellOrders, "Sell", "Supply");
                    break;
                default:
                    fragment = null;
                    break;
            }
            _fragmentReference[position] = fragment;
            return fragment;
        }

        public override ICharSequence GetPageTitleFormatted(int position)
        {
            switch (position)
            {
                case 0:
                    return new Java.Lang.String("General");
                case 1:
                    return new Java.Lang.String("Buy");
                case 2:
                    return new Java.Lang.String("Sell");
                default:
                    return new Java.Lang.String("untitled");
            }
        }

        public override int GetItemPosition(Object objectValue)
        {
            var value = objectValue as PriceListingFragment;
            if (value != null)
            {
                PriceListingFragment fragment = value;
                fragment?.Refresh();
            }

            return PositionNone;
        }
        

        public void RefreshFragment(int position)
        {
            IRefreshable fragment = (IRefreshable)_fragmentReference[position];
            fragment.Refresh();
        }
    }
}