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
using GW2Trader.Model;
using Android.Content.Res;

namespace GW2Trader_Android.Adapter
{
    public class ItemsAdapter : BaseAdapter<Item>
    {
        private Activity _activity;
        private List<Item> _items;

        public ItemsAdapter(Activity activity, List<Item> items)
            : base()
        {
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
            view.FindViewById<TextView>(Resource.Id.ItemName).Text = item.Name;
            view.FindViewById<TextView>(Resource.Id.Rarity).Text = item.Rarity;
            view.FindViewById<ImageView>(Resource.Id.Icon).SetImageResource(Resource.Drawable.placeholder);
            return view;
        }
    }
}