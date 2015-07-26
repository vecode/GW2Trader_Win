using System.Collections.Generic;
using Android.OS;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Adapter;
using GW2Trader.Model;
using Fragment = Android.Support.V4.App.Fragment;

namespace GW2Trader.Android.Fragments
{
    public class PriceListingFragment : Fragment, IRefreshable
    {
        private List<PriceListing> _priceListings; 
        private readonly string _kind;
        private readonly string _qtyName;
        private ListView _listingListView;
        private PriceListingAdapter _listingAdapter;

        public PriceListingFragment(List<PriceListing> priceListings, string kind, string qtyName)
        {
            _priceListings = priceListings;
            _kind = kind;
            _qtyName = qtyName;
        }

        public List<PriceListing> PriceListings
        {
            set
            {
                _priceListings = value;
                _listingAdapter?.NotifyDataSetChanged();
            }
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.PriceListing, container, false);

            _listingListView = view.FindViewById<ListView>(Resource.Id.PriceListingListView);
            _listingAdapter = new PriceListingAdapter(Activity, _priceListings);
            _listingListView.Adapter = _listingAdapter;

            var listingKind = view.FindViewById<TextView>(Resource.Id.ListingText);
            listingKind.Text = _kind;

            var listingQuantity = view.FindViewById<TextView>(Resource.Id.ListingQuantity);
            listingQuantity.Text = _qtyName;

            return view;
        }

        public override void OnResume()
        {
            base.OnResume();
            Refresh();
        }

        public void Refresh()
        {            
            if (_priceListings != null)
            {
                _listingAdapter?.NotifyDataSetChanged();
            }
        }
    }
}