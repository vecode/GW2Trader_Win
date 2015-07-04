using System;
using Android.App;
using Android.OS;
using Android.Widget;
using GW2Trader.Manager;
using TinyIoC;

namespace GW2Trader.Android.Activities
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

            var updateDbButton = FindViewById<Button>(Resource.Id.UpdateDbButton);
            updateDbButton.Click += OnUpdateDbButtonClick;
        }

        private void OnUpdateDbButtonClick(object sender, EventArgs e)
        {
            _itemManager.BuildItemDb();
        }
    }
}