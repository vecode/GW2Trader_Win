using System.Net;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Adapter;
using GW2Trader.Manager;
using GW2Trader.Model;
using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace GW2Trader.Android.Activities
{
    [Activity]
    public class ItemDetailsActivity : AppCompatActivity
    {
        private Item _item;
        private IItemManager _itemManager;

        private ViewPager _viewPager;
        private ItemDetailsFragmentAdapter _fragmentAdapter;
        private IMenu _menu;
        private IMenuItem _refreshMenuItem;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();

            var itemId = (int)Intent.GetLongExtra("ItemId", 0);
            _item = _itemManager.GetItem(itemId);

            SetContentView(Resource.Layout.ItemDetailsViewPager);

            var toolbar = FindViewById<Toolbar>(Resource.Id.ToolBar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            TextView textView = toolbar.FindViewById<TextView>(Resource.Id.Title);
            textView.Text = _item.Name;

            _fragmentAdapter = new ItemDetailsFragmentAdapter(SupportFragmentManager, _item);

            _viewPager = FindViewById<ViewPager>(Resource.Id.viewPager);
            _viewPager.OffscreenPageLimit = 2;
            _viewPager.Adapter = _fragmentAdapter;
            _viewPager.AddOnPageChangeListener(new PageListener(_fragmentAdapter));
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            _menu = menu;
            MenuInflater.Inflate(Resource.Menu.ItemDetailsMenu, menu);
            _refreshMenuItem = _menu.FindItem(Resource.Id.Action_ItemDetailsRefresh);

            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.Action_ItemDetailsRefresh:
                    _refreshMenuItem.SetActionView(Resource.Layout.ProgressSpinner);

                    ThreadPool.QueueUserWorkItem(x =>
                    {
                        try
                        {
                            UpdatePriceData();
                        }
                        catch (WebException ex)
                        {
                            RunOnUiThread(() =>
                            {
                                var toast = Toast.MakeText(this, "Update Failed", ToastLength.Short);
                                toast.SetGravity(GravityFlags.CenterHorizontal | GravityFlags.CenterVertical, 0, 0);
                                toast.Show();
                                _refreshMenuItem.SetActionView(null);
                            });
                        }
                    });
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void UpdatePriceData()
        {
            _itemManager.UpdatePrices(_item);
            _itemManager.UpdatePriceListings(_item);

            RunOnUiThread(() =>
                {
                    _fragmentAdapter.RefreshFragment(_viewPager.CurrentItem);
                    _refreshMenuItem.SetActionView(null);
                });
        }
    }

    class PageListener : ViewPager.SimpleOnPageChangeListener
    {
        private readonly ItemDetailsFragmentAdapter _fragmentAdapter;

        public PageListener(ItemDetailsFragmentAdapter fragmentAdapter)
        {
            _fragmentAdapter = fragmentAdapter;
        }

        public override void OnPageSelected(int position)
        {
            Fragment fragment = _fragmentAdapter.Fragment(position);
            _fragmentAdapter.NotifyDataSetChanged();
            fragment?.OnResume();
        }
    }
}