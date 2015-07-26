using System;
using Android.App;
using Android.OS;
using Android.Support.V4.App;
using Android.Support.V4.View;
using Android.Views;
using GW2Trader.Android.Adapter;
using GW2Trader.Android.Fragments;
using GW2Trader.Manager;
using GW2Trader.Model;
using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;

namespace GW2Trader.Android.Activities
{
    [Activity(Label = "Item Details")]
    public class ItemDetailsViewPagerActivity : FragmentActivity
    {
        private Item _item;
        private IItemManager _itemManager;

        private ViewPager _viewPager;
        private ItemDetailsFragmentAdapter _fragmentAdapter;
        private IMenu _menu;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();

            var itemId = (int)Intent.GetLongExtra("ItemId", 0);
            _item = _itemManager.GetItem(itemId);            

            SetContentView(Resource.Layout.ItemDetailsViewPager);

            _fragmentAdapter = new ItemDetailsFragmentAdapter(SupportFragmentManager, _item);

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            //_viewPager.OffscreenPageLimit = 0;
            _viewPager.Adapter = _fragmentAdapter;
            _viewPager.AddOnPageChangeListener(new PageListener(_fragmentAdapter));

        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _menu = menu;
            MenuInflater inflater = MenuInflater;
            inflater.Inflate(Resource.Menu.ItemDetailsMenu, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Itemdetails_Refresh:
                    UpdatePriceData();
                    //_fragmentAdapter.RefreshFragment(_viewPager.CurrentItem);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void UpdatePriceData()
        {
            _itemManager.UpdatePrices(_item);
            _itemManager.UpdatePriceListings(_item);
        }
    }

    class PageListener : ViewPager.SimpleOnPageChangeListener
    {
        private ItemDetailsFragmentAdapter _fragmentAdapter;

        public PageListener(ItemDetailsFragmentAdapter fragmentAdapter)
        {
            _fragmentAdapter = fragmentAdapter;
        }

        public override void OnPageSelected(int position)
        {
            _fragmentAdapter.NotifyDataSetChanged();
            Console.WriteLine("on page selected " + position.ToString());
            IRefreshable fragment = (IRefreshable)_fragmentAdapter.GetItem(position);
            //fragment?.OnResume();
            fragment?.Refresh();            
        }
    }
}