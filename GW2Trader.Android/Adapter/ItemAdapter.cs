using System.Collections.Generic;
using System.Threading;
using Android.App;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Util;
using GW2Trader.Model;
using TinyIoC;

namespace GW2Trader.Android.Adapter
{
    public class ItemAdapter : BaseAdapter<Item>
    {
        private readonly Activity _activity;
        private readonly IIconStore _iconStore;
        private readonly List<Item> _items;

        public ItemAdapter(Activity activity, List<Item> items)
        {
            _iconStore = TinyIoCContainer.Current.Resolve<IIconStore>();
            _activity = activity;
            _items = items;
        }

        public override Item this[int position] => _items[position];

        public override int Count => _items.Count;

        public override long GetItemId(int position)
        {
            return _items[position].Id;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = _items[position];
            var view = convertView ?? _activity.LayoutInflater.Inflate(Resource.Layout.SearchResultListViewItem, null);
            view.FindViewById<TextView>(Resource.Id.Name).Text = item.Name;

            var iconView = view.FindViewById<ImageView>(Resource.Id.Icon);

            ThreadPool.QueueUserWorkItem(x => _iconStore.SetIcon(item, iconView, _activity));

            return view;
        }

        public List<Item> GetItems()
        {
            return _items;
        }
    }
}