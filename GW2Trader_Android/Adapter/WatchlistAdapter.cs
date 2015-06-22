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
    public class WatchlistAdapter : BaseAdapter<Watchlist>
    {
        private List<Watchlist> _watchlists;
        private Activity _activity;

        public WatchlistAdapter(Activity activity, List<Watchlist> watchlists)
        {
            _activity = activity;
            _watchlists = watchlists;
        }

        public override Watchlist this[int position]
        {
            get { return _watchlists[position]; }
        }

        public override int Count
        {
            get { return _watchlists.Count; }
        }

        public override long GetItemId(int position)
        {
            return _watchlists[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Watchlist watchlist = _watchlists[position];
            View view = convertView;
            if (view == null)
                view = _activity.LayoutInflater.Inflate(Resource.Layout.WatchlistViewItem, null);

            view.FindViewById<TextView>(Resource.Id.Name).Text = watchlist.Name;
            return view;
        }
    }
}