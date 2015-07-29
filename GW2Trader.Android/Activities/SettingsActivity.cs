using System;
using Android.App;
using Android.OS;
using Android.Widget;
using GW2Trader.Manager;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using TinyIoC;

namespace GW2Trader.Android.Activities
{
    [Activity]
    public class SettingsActivity : AppCompatActivity
    {
        private IItemManager _itemManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Settings);

            var container = TinyIoCContainer.Current;
            _itemManager = container.Resolve<IItemManager>();

            var toolbar = FindViewById<Toolbar>(Resource.Id.ToolBar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            TextView textView = toolbar.FindViewById<TextView>(Resource.Id.Title);
            textView.Text = "Settings";

            var updateDbButton = FindViewById<Button>(Resource.Id.UpdateDbButton);
            updateDbButton.Click += OnUpdateDbButtonClick;
        }

        private void OnUpdateDbButtonClick(object sender, EventArgs e)
        {
            _itemManager.BuildItemDb();
        }
    }
}