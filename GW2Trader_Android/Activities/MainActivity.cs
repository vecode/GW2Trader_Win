﻿using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using GW2Trader_Android.Util;
using GW2Trader_Android.Util.OfflineTest;
using System.IO;

namespace GW2Trader_Android.Activities
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

