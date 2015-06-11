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

namespace GW2Trader_Android.Adapter
{
    public class GameItemAdapter : PaginatedListAdapter<Item>
    {
        public GameItemAdapter(Activity activity, List<Item> items, int pageSize = 10)
            : base(activity, items, pageSize) { }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            Item item = this[position];
            View view = convertView;
            if (view == null) // no view to re-use, create new
                view = _activity.LayoutInflater.Inflate(Resource.Layout.SearchResultListViewItem, null);            
            
            view.FindViewById<TextView>(Resource.Id.Name).Text = item.Name;
            view.FindViewById<ImageView>(Resource.Id.Icon).SetImageResource(Resource.Drawable.placeholder);
            return view;
        }
    }
}