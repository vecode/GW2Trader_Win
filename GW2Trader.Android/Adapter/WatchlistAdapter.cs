using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;

namespace GW2Trader.Android.Adapter
{
    public class WatchlistAdapter : BaseAdapter<Watchlist>
    {
        private readonly Activity _activity;
        private readonly List<Watchlist> _watchlists;

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
            var watchlist = _watchlists[position];
            var view = convertView;
            if (view == null)
                view = _activity.LayoutInflater.Inflate(Resource.Layout.WatchlistListViewItem, null);

            view.FindViewById<TextView>(Resource.Id.Name).Text = watchlist.Name;
            return view;
        }
    }
}