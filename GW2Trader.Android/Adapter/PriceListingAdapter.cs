using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Util.UI;
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
            _priceListings = priceListings ?? new List<PriceListing>();
        }

        public List<PriceListing> GetItems()
        {
            return _priceListings;
        }

        public override PriceListing this[int position] => _priceListings[position];

        public override int Count => _priceListings?.Count ?? 0;

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var listing = _priceListings[position];

            var view = convertView ?? _activity.LayoutInflater
                .Inflate(Resource.Layout.PriceListingListViewItem, null);

            view.FindViewById<TextView>(Resource.Id.Quantity).Text = listing.Quantity.ToString();
            LinearLayout priceLayout = view.FindViewById<LinearLayout>(Resource.Id.Price);
            MoneyViewSetter.SetMoneyView(priceLayout, listing.Price);
            return view;
        }
    }
}