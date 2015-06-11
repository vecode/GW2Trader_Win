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

namespace GW2Trader_Android.Adapter
{
    public abstract class PaginatedListAdapter<T> : BaseAdapter<T>
    {
        protected Activity _activity;
        private List<T> _items;

        private int _pageSize;
        private int _pageCount;
        public int CurrentPage { get; private set; }

        public PaginatedListAdapter(Activity activity, List<T> items, int pageSize = 10)
            :base()
        {
            _activity = activity;
            _items = items;
            _pageSize = pageSize;
            _pageCount = (int)Math.Ceiling(items.Count / (double)pageSize);
            CurrentPage = 0;
        }

        public override T this[int position]
        {
            get { return _items[(_pageSize * CurrentPage) + position]; }
        }

        public override int Count
        {
            get { return Math.Min(_pageSize, _items.Count - (_pageSize * CurrentPage)); }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            throw new NotImplementedException();
        }

        public void MoveToNextPage()
        {
            if (CanMoveForward) 
            { 
                CurrentPage++;
                NotifyDataSetChanged();
            }
        }

        public void MoveToPreviousPage()
        {
            if (CanMoveBack)
            {
                CurrentPage--;
                NotifyDataSetChanged();
            }
        }

        public bool CanMoveForward
        {
            get { return CurrentPage < _pageCount - 1; }
        }

        public bool CanMoveBack
        {
            get { return CurrentPage > 0; }
        }

    }
}