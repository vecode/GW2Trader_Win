using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;
using GW2Trader.Manager;
using GW2Trader_Android.Adapter;

namespace GW2Trader_Android.Fragments
{
    public class PriceListingFragment : Fragment
    {
        private IItemManager _itemManager;
        private Item _item;
        private string _kind;
        private string _qtyName;

        private ListView _listingListView;

        public PriceListingFragment(Item item, string kind, string qtyName)
        {
            _item = item;
            _kind = kind;
            _qtyName = qtyName;

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.PriceListing, container, false);

            _listingListView = view.FindViewById(Resource.Id.PriceListingListView);
            _listingListView.Adapter = new PriceListingAdapter(Activity.LayoutInflater, _item.)

            var listingKind = view.FindViewById<TextView>(Resource.Id.ListingKind);
            listingKind.Text = _kind;

            var listingQuantity = view.FindViewById<TextView>(Resource.Id.ListingQuantity);
            listingQuantity.Text = _qtyName;

            return view;
        }

        private void SetPriceListings(Item item)
        {
            
        }
    }
}