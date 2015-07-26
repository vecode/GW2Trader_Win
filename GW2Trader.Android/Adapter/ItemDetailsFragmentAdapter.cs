using System.Collections.Generic;
using Android.Support.V4.App;
using Android.Views;
using GW2Trader.Android.Fragments;
using GW2Trader.Model;
using Fragment = Android.Support.V4.App.Fragment;

namespace GW2Trader.Android.Adapter
{
    public class ItemDetailsFragmentAdapter : FragmentPagerAdapter
    {
        public override int Count => 3;
        private readonly Item _item;
        private readonly Dictionary<int, string> _fragmentTagMap = new Dictionary<int, string>();
        private readonly FragmentManager _fragmentManager;

        public ItemDetailsFragmentAdapter(FragmentManager fm, Item item)
            : base(fm)
        {
            _item = item;
            _fragmentManager = fm;
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
            return fragment;
        }

        public override Java.Lang.ICharSequence GetPageTitleFormatted(int position)
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

        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            Java.Lang.Object obj = base.InstantiateItem(container, position);
            Fragment fragment = (Fragment)obj;
            if (fragment != null)
            {
                string tag = fragment.Tag;
                _fragmentTagMap[position] = tag;
            }
            return obj;
        }

        public Fragment Fragment(int position)
        {
            string tag;
            if (_fragmentTagMap.TryGetValue(position, out tag))
            {
                return _fragmentManager.FindFragmentByTag(tag);
            }
            return null;
        }

        public void RefreshFragment(int position)
        {
            IRefreshable fragment = (IRefreshable) Fragment(position);
            fragment?.Refresh();
        }
    }
}