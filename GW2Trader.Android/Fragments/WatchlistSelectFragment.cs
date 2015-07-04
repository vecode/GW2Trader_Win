using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Adapter;
using GW2Trader.Model;

namespace GW2Trader.Android.Fragments
{
    public class WatchlistSelectFragment : DialogFragment
    {
        private readonly List<Watchlist> _watchlists =
            new List<Watchlist>
            {
                new Watchlist {Id = 1, Name = "WL 1"},
                new Watchlist {Id = 2, Name = "WL 2"},
                new Watchlist {Id = 3, Name = "WL 3"}
            };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = inflater.Inflate(Resource.Layout.WatchlistSelect, container);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            var watchlistListView = view.FindViewById<ListView>(Resource.Id.WatchlistListView);
            watchlistListView.Adapter = new WatchlistAdapter(Activity, _watchlists);
            return view;
        }
    }
}