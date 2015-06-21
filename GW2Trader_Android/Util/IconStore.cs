using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
using GW2Trader.Model;
using System.Net;
using System.Diagnostics;
using System.Collections.Concurrent;
using System.ComponentModel;

namespace GW2Trader_Android.Util
{
    public class IconStore : IIconStore
    {
        private string _iconDirectory;
        private Queue<IconToLoad> _iconsToLoad = new Queue<IconToLoad>();
        private BackgroundWorker _backgroundWorker = new BackgroundWorker();

        public IconStore(string iconDirectory)
        {
            _iconDirectory = iconDirectory;
            _backgroundWorker.DoWork += ProcessQueue;
        }

        private void ProcessQueue(object sender, DoWorkEventArgs e)
        {
            while (_iconsToLoad.Count > 0)
            {
                LoadNext();
            }
        }

        public void SetIcon(Item item, ImageView view, Activity activity)
        {
            string path = GetIconPath(item);
            if (File.Exists(path))
            {
                Bitmap icon = BitmapFactory.DecodeFile(path);

                if (view != null & activity != null)
                {
                    activity.RunOnUiThread(
                        () => view.SetImageBitmap(BitmapFactory.DecodeFile(path)));
                }
                return;
            }
            else
            {
                _iconsToLoad.Enqueue
                (
                    new IconToLoad 
                    { 
                        Item = item, 
                        ImageView = view,
                        Activity = activity
                    }
                );

                lock (_backgroundWorker)
                {
                    if (!_backgroundWorker.IsBusy)
                    {
                        _backgroundWorker.RunWorkerAsync();
                    }
                }                
            }
        }

        private void LoadNext()
        {
            IconToLoad next = _iconsToLoad.Dequeue();
            if (next != null)
            {
                string path = GetIconPath(next.Item);
                string tmpFilePath = path + ".tmp";

                Directory.CreateDirectory(_iconDirectory);
                if (!File.Exists(path))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFile(next.Item.IconUrl, tmpFilePath);
                        File.Move(tmpFilePath, path);
                    }
                }
                //if (next.ImageView != null & next.Activity != null)
                //{
                //    next.Activity.RunOnUiThread(
                //        () => next.ImageView.SetImageBitmap(BitmapFactory.DecodeFile(path)));
                //}
                SetIcon(next.Item, next.ImageView, next.Activity);
                //next.ImageView.SetImageBitmap(BitmapFactory.DecodeFile(path));
            }
        }

        private string GetIconPath(Item item)
        {
            return System.IO.Path.Combine(_iconDirectory, System.IO.Path.GetFileName(item.IconUrl));
        }

        private class IconToLoad
        {
            public Item Item { get; set; }
            public ImageView ImageView { get; set; }
            public Activity Activity { get; set; }
        }
    }
}