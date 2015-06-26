using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;

namespace GW2Trader_Android.Adapter
{
    public class PriceListingAdapter : BaseAdapter<PriceListing>
    {
        private Activity _activity;
        private List<PriceListing> _priceListings;

        public PriceListingAdapter(Activity activity, List<PriceListing> priceListings)
        {
            _activity = activity;
            _priceListings = priceListings;
        }

        public override PriceListing this[int position]
        {
            get { return _priceListings[position]; }
        }

        public override int Count
        {
            get { return _priceListings.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            PriceListing listing = _priceListings[position];

            View view = convertView;
            if (view == null)
                view = _activity.LayoutInflater.Inflate(Resource.Layout.PriceListingListViewItem, null);

            view.FindViewById<TextView>(Resource.Id.Quantity).Text = listing.Quantity.ToString();

            return view;
        }
    }
}