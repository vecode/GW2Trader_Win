using System;
using System.Threading;
using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using GW2Trader.Manager;
using GW2Trader.Model;
using GW2Trader.Util;
using TinyIoC;
using IIconStore = GW2Trader.Android.Util.IIconStore;

namespace GW2Trader.Android.Fragments
{
    public class ItemDetailsFragment : Fragment
    {
        private readonly Item _item;
        private LinearLayout _buyLayout;
        private TextView _demandTextView;
        private ImageView _iconImageView;
        private IIconStore _iconStore;
        private IItemManager _itemManager;
        private TextView _lastUpdateTextView;
        private LinearLayout _marginLayout;
        private TextView _nameTextView;
        private TextView _rarityTextView;
        private TextView _roiTextView;
        private LinearLayout _sellLayout;
        private TextView _subtypeTextView;
        private TextView _supplyTextView;
        private TextView _typeTextView;

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

            var view = inflater.Inflate(Resource.Layout.ItemDetails, container, false);

            FindViews(view);

            SetItemDetails(_item);

            ThreadPool.QueueUserWorkItem(x => { SetIcon(_item); });

            ThreadPool.QueueUserWorkItem(x =>
            {
                _itemManager.UpdatePrices(_item);
                if (Activity != null)
                    Activity.RunOnUiThread(() => SetItemDetails(_item));
            });

            var watchButton = view.FindViewById<Button>(Resource.Id.Watch);
            watchButton.Click += OnWatchButtonClicked;

            return view;
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

            _sellLayout = view.FindViewById<LinearLayout>(Resource.Id.SellPrice);
            _buyLayout = view.FindViewById<LinearLayout>(Resource.Id.BuyPrice);
            _marginLayout = view.FindViewById<LinearLayout>(Resource.Id.Margin);
        }

        private void SetItemDetails(Item item)
        {
            _nameTextView.Text = item.Name;
            _typeTextView.Text = item.Type;
            _subtypeTextView.Text = item.SubType;
            _rarityTextView.Text = item.Rarity;
            _roiTextView.Text = "";

            _demandTextView.Text = item.Demand.ToString();
            _supplyTextView.Text = item.Supply.ToString();
            _lastUpdateTextView.Text = item.CommerceDataLastUpdated.ToString();

            SetMoneyView(_sellLayout, _item.SellPrice);
            SetMoneyView(_buyLayout, _item.BuyPrice);
            SetMoneyView(_marginLayout, _item.Margin);
        }

        private void SetMoneyView(LinearLayout layout, int value)
        {
            var goldShare = value < 0 ? "-" : string.Empty;
            goldShare += MoneyHelper.ExtractGoldShare(value).ToString();
            layout.FindViewById<TextView>(Resource.Id.Gold).Text = goldShare;

            layout.FindViewById<TextView>(Resource.Id.Silver).Text = MoneyHelper.ExtractSilverShare(value).ToString();
            layout.FindViewById<TextView>(Resource.Id.Copper).Text = MoneyHelper.ExtractCopperShare(value).ToString();
        }

        private void OnWatchButtonClicked(object sender, EventArgs e)
        {
            var transaction = FragmentManager.BeginTransaction();
            var watchlistSelectDialog = new WatchlistSelectFragment();
            watchlistSelectDialog.Show(transaction, "test");
        }

        private void SetIcon(Item item)
        {
            _iconStore.SetIcon(item, _iconImageView, Activity);
        }
    }
}