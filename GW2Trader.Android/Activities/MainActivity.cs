using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using GW2Trader.Android.Util;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace GW2Trader.Android.Activities
{
    [Activity(MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            IAppConfig appConfig = new AppConfig();
            //IAppConfig appConfig = new OfflineAppConfig();

            appConfig.Initialize();

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var toolbar = FindViewById<Toolbar>(Resource.Id.ToolBar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            TextView textView = toolbar.FindViewById<TextView>(Resource.Id.Title);
            textView.Text = "GW2Trader";

            var watchlistsButton = FindViewById<LinearLayout>(Resource.Id.WatchlistButton);
            watchlistsButton.Click += OnWatchlistsButtonClicked;

            var settingsButton = FindViewById<LinearLayout>(Resource.Id.SettingsButton);
            settingsButton.Click += OnSettingsButtonClicked;

            var searchButton = FindViewById<LinearLayout>(Resource.Id.SearchButton);
            searchButton.Click += OnSearchButtonClicked;

            var debugButton = FindViewById<Button>(Resource.Id.DebugButton);
            debugButton.Click += OnDebugButtonClicked;
        }

        private void OnDebugButtonClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (DebugActivity));
            StartActivity(intent);
        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (SettingsActivity));
            StartActivity(intent);
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (SearchActivity));
            StartActivity(intent);
        }

        private void OnWatchlistsButtonClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (WatchlistActivity));
            StartActivity(intent);
        }
    }
}