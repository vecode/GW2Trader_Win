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
using Android.Content.Res;
using System.Net;

namespace GW2Trader_Android.Util
{
    public class IconStore : IIconStore
    {
        private Dictionary<string, Bitmap> _icons;
        private string _localDirectory;

        public IconStore(string localDirectory)
        {
            _localDirectory = localDirectory;
            _icons = new Dictionary<string, Bitmap>();
        }

        public Bitmap GetIcon(Item item)
        {
            Bitmap icon;
            _icons.TryGetValue(item.IconUrl, out icon);
            return icon;
        }

        public bool HasIconForItem(Item item)
        {
            return _icons.ContainsKey(item.IconUrl);
        }

        public void AddIconForItem(Item item, Bitmap icon)
        {
            if (!_icons.ContainsKey(item.IconUrl))
            {
                _icons.Add(item.IconUrl, icon);
            }
        }


        public void DownloadIcon(Item item)
        {
            string path = System.IO.Path.Combine(_localDirectory, System.IO.Path.GetFileName(item.IconUrl));

            using (var webClient = new WebClient())
            {
                webClient.DownloadFile(item.IconUrl, path);
            }

            Bitmap bitmap = BitmapFactory.DecodeFile(path);
            _icons.Add(item.IconUrl, bitmap);
        }

        private Bitmap GetImageFromUrl(string url)
        {
            using (var webClient = new WebClient())
            {
                byte[] iconByteArray = webClient.DownloadData(url);
                return BitmapFactory.DecodeByteArray(iconByteArray, 0, iconByteArray.Length);
            }
        }
    }
}