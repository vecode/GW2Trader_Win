using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;
using TinyIoC;
using GW2Trader.Manager;
using System.Net;
using Android.Graphics;

namespace GW2Trader_Android.Fragments
{
    public class ItemDetails : Fragment
    {
        private IItemManager _itemManager;
        private Util.IIconStore _iconStore;
        private Item _item;

        private ImageView _iconImageView;
        private TextView _nameTextView;
        private TextView _rarityTextView;
        private TextView _typeTextView;
        private TextView _subtypeTextView;
        private TextView _roiTextView;
        private TextView _demandTextView;
        private TextView _supplyTextView;
        private TextView _lastUpdateTextView;

        private LinearLayout _sellLayout;
        private LinearLayout _buyLayout;
        private LinearLayout _marginLayout;

        public ItemDetails(Item item)
            : base()
        {
            _item = item;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();
            _iconStore = TinyIoCContainer.Current.Resolve<Util.IIconStore>();
            base.OnCreate(savedInstanceState);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ItemDetails, container, false);

            FindViews(view);

            SetItemDetails(_item);

            ThreadPool.QueueUserWorkItem(x =>
            {
                SetIcon(_item);
            });

            ThreadPool.QueueUserWorkItem(x =>
            {
                _itemManager.UpdatePrices(_item);
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
            layout.FindViewById<TextView>(Resource.Id.Gold).Text = "--";
            layout.FindViewById<TextView>(Resource.Id.Silver).Text = "--";
            layout.FindViewById<TextView>(Resource.Id.Copper).Text = "--";
        }

        private void OnWatchButtonClicked(object sender, EventArgs e)
        {
            var transaction = FragmentManager.BeginTransaction();
            var watchlistSelectDialog = new WatchlistSelect();
            watchlistSelectDialog.Show(transaction, "test");
        }

        private void SetIcon(Item item)
        {
            if (_iconImageView.Drawable != null) { return; }

            Bitmap icon;
            if (!_iconStore.HasIconForItem(item))
            {
                _iconStore.DownloadIcon(item);
            }
            icon = _iconStore.GetIcon(item);
            
            Activity.RunOnUiThread(() => _iconImageView.SetImageBitmap(icon));
        }
    }
}