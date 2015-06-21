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
using System.IO;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "DebugActivity")]
    public class DebugActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.DebugLayout);

            Button deleteIconsButton = FindViewById<Button>(Resource.Id.DeleteIconsButton);
            deleteIconsButton.Click += OnDeleteIconsButtonClicked;
        }

        private void OnDeleteIconsButtonClicked(object sender, EventArgs e)
        {
            string iconDirectory = System.IO.Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath, "GW2Trader", "Icons");

            foreach (var file in Directory.EnumerateFiles(iconDirectory))
            {
                File.Delete(file);
            }
            Directory.Delete(iconDirectory);
        }

    }
}