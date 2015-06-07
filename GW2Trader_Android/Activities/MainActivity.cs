using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "GW2Trader", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        int count = 1;

        protected override void OnCreate(Bundle bundle)
        {
            App.Initialize();

            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            Button button = FindViewById<Button>(Resource.Id.SettingsButton);
            button.Click += OnSettingsButtonClicked;
        }

        private void OnSettingsButtonClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof(SettingsActivity));
            StartActivity(intent);
        }
    }
}

