using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GW2Trader.Android.Util.UI;
using GW2Trader.Manager;
using GW2Trader.Model;
using TinyIoC;
using Fragment = Android.Support.V4.App.Fragment;
using IIconStore = GW2Trader.Android.Util.IIconStore;

namespace GW2Trader.Android.Fragments
{
    public class ItemDetailsFragment : Fragment, IRefreshable
    {
        private readonly Item _item;
        private LinearLayout _buyPriceLayout;
        private LinearLayout _sellPriceLayout;
        private LinearLayout _marginLayout;
        private TextView _demandTextView;
        private TextView _supplyTextView;

        // TODO remove?
        private View _view;

        private TextView _lastUpdateTextView;
        private TextView _nameTextView;
        private TextView _rarityTextView;
        private TextView _roiTextView;
        private TextView _typeTextView;
        private TextView _subtypeTextView;
        private ImageView _iconImageView;

        private IIconStore _iconStore;
        private IItemManager _itemManager;                       

        public ItemDetailsFragment(Item item)
        {
            _item = item;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();
            _iconStore = TinyIoCContainer.Current.Resolve<IIconStore>();
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            View view = inflater.Inflate(Resource.Layout.ItemDetails, container, false);
            _view = view;
            FindViews(view);

            SetItemDetails(_item);

            ThreadPool.QueueUserWorkItem(x => { SetIcon(_item); });

            //ThreadPool.QueueUserWorkItem(x =>
            //{
            //    _itemManager.UpdatePrices(_item);
            //    Activity?.RunOnUiThread(() => SetItemDetails(_item));
            //});

            var watchButton = view.FindViewById<Button>(Resource.Id.Watch);
            watchButton.Click += OnWatchButtonClicked;
            Console.WriteLine("view created");
            return view;
        }

        public void Refresh()
        {
            SetItemDetails(_item);
        }

        private void FindViews(View view)
        {
            _iconImageView = view.FindViewById<ImageView>(Resource.Id.Icon);
            _nameTextView = view.FindViewById<TextView>(Resource.Id.Name);
            _typeTextView = view.FindViewById<TextView>(Resource.Id.Type);
            _subtypeTextView = view.FindViewById<TextView>(Resource.Id.SubType);
            _rarityTextView = view.FindViewById<TextView>(Resource.Id.Rarity);
            _supplyTextView = view.FindViewById<TextView>(Resource.Id.Supply);
            _demandTextView = view.FindViewById<TextView>(Resource.Id.Demand);
            _lastUpdateTextView = view.FindViewById<TextView>(Resource.Id.LastUpdate);

            _roiTextView = view.FindViewById<TextView>(Resource.Id.Roi);

            _sellPriceLayout = view.FindViewById<LinearLayout>(Resource.Id.SellPrice);
            _buyPriceLayout = view.FindViewById<LinearLayout>(Resource.Id.BuyPrice);
            _marginLayout = view.FindViewById<LinearLayout>(Resource.Id.Margin);
        }

        private void SetItemDetails(Item item)
        {
            if (_nameTextView == null)
            {
                return;}
            _nameTextView.Text = item.Name;
            _typeTextView.Text = item.Type;
            _subtypeTextView.Text = item.SubType;
            _rarityTextView.Text = item.Rarity;
            _roiTextView.Text = "";

            _demandTextView.Text = item.Demand.ToString();
            _supplyTextView.Text = item.Supply.ToString();
            _lastUpdateTextView.Text = item.CommerceDataLastUpdated.ToString();

            MoneyViewSetter.SetMoneyView(_sellPriceLayout, _item.SellPrice);
            MoneyViewSetter.SetMoneyView(_buyPriceLayout, _item.BuyPrice);
            MoneyViewSetter.SetMoneyView(_marginLayout, _item.Margin);
        }        

        private void OnWatchButtonClicked(object sender, EventArgs e)
        {
            var transaction = FragmentManager.BeginTransaction();
            var watchlistSelectDialog = new WatchlistSelectFragment();
            //watchlistSelectDialog.Show(transaction, "test");
        }

        private void SetIcon(Item item)
        {
            _iconStore.SetIcon(item, _iconImageView, Activity);
        }

        public override void OnResume()
        {
            base.OnResume();
        }
    }
}