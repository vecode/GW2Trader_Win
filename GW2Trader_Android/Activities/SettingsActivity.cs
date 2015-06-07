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
using TinyIoC;
using GW2Trader.Manager;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        private IItemManager _itemManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);

            var container = TinyIoCContainer.Current;
            _itemManager = container.Resolve<IItemManager>();

            Button updateDbButton = FindViewById<Button>(Resource.Id.UpdateDbButton);
            updateDbButton.Click += OnUpdateDbButtonClick;
        }

        private void OnUpdateDbButtonClick(object sender, EventArgs e)
        {
            _itemManager.BuildItemDb();
        }
    }
}