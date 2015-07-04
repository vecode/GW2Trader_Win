using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Widget;
using Environment = Android.OS.Environment;

namespace GW2Trader.Android.Activities
{
    [Activity(Label = "DebugActivity")]
    public class DebugActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DebugLayout);

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