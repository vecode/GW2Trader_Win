using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;
using TinyIoC;
using System.Threading;
using Android.Graphics;

namespace GW2Trader_Android.Adapter
{
    public class ItemAdapter : BaseAdapter<Item>
    {
        private Activity _activity;
        private List<Item> _items;
        private Util.IIconStore _iconStore;

        public ItemAdapter(Activity activity, List<Item> items)
        {
            _iconStore = TinyIoCContainer.Current.Resolve<Util.IIconStore>();
            _activity = activity;
            _items = items;
        }

        public override Item this[int position]
        {
            get { return _items[position]; }
        }

        public override int Count
        {
            get { return _items.Count; }
        }

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Item item = _items[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = _activity.LayoutInflater.Inflate(Resource.Layout.SearchResultListViewItem, null);
            view.FindViewById<TextView>(Resource.Id.Name).Text = item.Name;

            ImageView iconView = view.FindViewById<ImageView>(Resource.Id.Icon);

            ThreadPool.QueueUserWorkItem(x => _iconStore.SetIcon(item, iconView, _activity));

            return view;
        }

        public List<Item> GetItems()
        {
            return _items;
        }
    }
}