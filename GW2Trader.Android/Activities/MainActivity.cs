using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using GW2Trader.Android.Util;

namespace GW2Trader.Android.Activities
{
    [Activity(Label = "GW2Trader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            IAppConfig appConfig = new AppConfig();
            //IAppConfig appConfig = new OfflineAppConfig();

            appConfig.Initialize();

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var settingsButton = FindViewById<Button>(Resource.Id.SettingsButton);
            settingsButton.Click += OnSettingsButtonClicked;

            var searchButton = FindViewById<Button>(Resource.Id.SearchButton);
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
    }
}