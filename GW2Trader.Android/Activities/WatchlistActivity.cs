using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Adapter;
using GW2Trader.Manager;
using GW2Trader.Model;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using TinyIoC;

namespace GW2Trader.Android.Activities
{
    [Activity]
    public class WatchlistActivity : AppCompatActivity
    {
        private IWatchlistManager _watchlistManager;
        private List<Watchlist> _watchlists;

        private ListView _listView;
        private WatchlistAdapter _watchlistAdapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            _watchlists = Watchlists();

            InitUI();
        }

        private void InitUI()
        {
            SetContentView(Resource.Layout.Watchlist);

            var toolbar = FindViewById<Toolbar>(Resource.Id.ToolBar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            TextView textView = toolbar.FindViewById<TextView>(Resource.Id.Title);
            textView.Text = "Watchlists";

            _listView = FindViewById<ListView>(Resource.Id.WatchlistListView);
            _watchlistAdapter = new WatchlistAdapter(this, _watchlists);
            _listView.Adapter = _watchlistAdapter;
            _listView.ItemClick += OnWatchlistClicked;
        }

        private void OnWatchlistClicked(object sender, EventArgs e)
        {
            // TODO implement this
        }

        private List<Watchlist> Watchlists()
        {
            var watchlists = new List<Watchlist>();

            Enumerable.Range(0, 3).ToList().ForEach(x => watchlists.AddRange(
                 new List<Watchlist>()
             {
                new Watchlist() {Description = "some materials", Id = 1, Name = "Materials"},
                new Watchlist() {Description = "some stuff", Id = 2, Name = "Weapons"},
                new Watchlist() {Description = "some skins", Id = 3, Name = "Super Rare Skins!"}
             }));
            return watchlists;
        }
    }
}