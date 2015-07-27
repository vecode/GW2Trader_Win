using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Environment = Android.OS.Environment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace GW2Trader.Android.Activities
{
    [Activity]
    public class DebugActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DebugLayout);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Debug";

            var deleteIconsButton = FindViewById<Button>(Resource.Id.DeleteIconsButton);
            deleteIconsButton.Click += OnDeleteIconsButtonClicked;
        }

        private void OnDeleteIconsButtonClicked(object sender, EventArgs e)
        {
            var iconDirectory = Path.Combine(Environment.ExternalStorageDirectory.AbsolutePath, "GW2Trader", "Icons");

            foreach (var file in Directory.EnumerateFiles(iconDirectory))
            {
                File.Delete(file);
            }
            Directory.Delete(iconDirectory);
        }
    }
}