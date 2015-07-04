using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;

namespace GW2Trader.Android.Adapter
{
    public class PriceListingAdapter : BaseAdapter<PriceListing>
    {
        private readonly Activity _activity;
        private readonly List<PriceListing> _priceListings;

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
            var listing = _priceListings[position];

            var view = convertView;
            if (view == null)
                view = _activity.LayoutInflater.Inflate(Resource.Layout.PriceListingListViewItem, null);

            view.FindViewById<TextView>(Resource.Id.Quantity).Text = listing.Quantity.ToString();

            return view;
        }
    }
}