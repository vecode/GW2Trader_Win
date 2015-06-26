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
using GW2Trader_Android.Adapter;
using GW2Trader.Model;

namespace GW2Trader_Android.Fragments
{
    public class WatchlistSelectFragment : DialogFragment
    {
        List<Watchlist> _watchlists =
            new List<Watchlist>
            { 
                new Watchlist {Id=1, Name="WL 1"},
                new Watchlist {Id=2, Name="WL 2"},
                new Watchlist {Id=3, Name="WL 3"}
            };


        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.WatchlistSelect, container);
            Dialog.Window.RequestFeature(WindowFeatures.NoTitle);
            ListView watchlistListView = view.FindViewById<ListView>(Resource.Id.WatchlistListView);
            watchlistListView.Adapter = new WatchlistAdapter(Activity, _watchlists);
            return view;
        }
    }
}