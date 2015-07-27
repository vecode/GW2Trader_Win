using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using GW2Trader.Android.Adapter;
using GW2Trader.Manager;
using GW2Trader.Model;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using TinyIoC;

namespace GW2Trader.Android.Activities
{
    [Activity]
    public class SearchResultActivity : AppCompatActivity
    {
        private const int PageSize = 25;
        private int _currentPage;
        private IItemManager _itemManager;
        private List<Item> _items;
        private ItemAdapter _itemsAdapter;
        private ListView _listView;
        private ImageButton _nextPageButton;
        private ImageButton _previousPageButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();

            _items = Search();

            InitUI();
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Finish();
        }

        private void InitUI()
        {
            SetContentView(Resource.Layout.SearchResult);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.Title = "Search Result";

            _listView = FindViewById<ListView>(Resource.Id.SearchResultListView);

            _itemsAdapter = new ItemAdapter(this, _items);
            _listView.Adapter = _itemsAdapter;
            _listView.ItemClick += OnItemClick;

            _previousPageButton = FindViewById<ImageButton>(Resource.Id.PreviousButton);
            _previousPageButton.Click += OnPreviousButtonClicked;

            _nextPageButton = FindViewById<ImageButton>(Resource.Id.NextButton);
            _nextPageButton.Click += OnNextButtonClicked;
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            _currentPage++;
            var nextItems = Search();

            if (nextItems.Any())
            {
                _itemsAdapter.GetItems().Clear();
                _itemsAdapter.GetItems().AddRange(nextItems);
                _itemsAdapter.NotifyDataSetChanged();
                _listView.SetSelectionAfterHeaderView();
            }
            else
            {
                _nextPageButton.Clickable = false;
                _currentPage--;
            }

            if (_currentPage > 0)
            {
                _previousPageButton.Clickable = true;
            }
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            if (_currentPage <= 0) return;
            _currentPage--;

            var previousItems = Search();
            if (previousItems.Any())
            {
                _itemsAdapter.GetItems().Clear();
                _itemsAdapter.GetItems().AddRange(previousItems);
                _itemsAdapter.NotifyDataSetChanged();
                _listView.SetSelectionAfterHeaderView();
            }
            _nextPageButton.Clickable = true;
        }

        private List<Item> Search()
        {
            var query = Intent.GetStringExtra("Query");

            var rarity = Intent.GetStringExtra("Rarity");
            if (rarity.ToLower().Equals("all"))
            {
                rarity = "";
            }

            var type = Intent.GetStringExtra("Type");
            if (type.ToLower().Equals("all"))
            {
                type = "";
            }

            int minLvl;
            int.TryParse(Intent.GetStringExtra("minLevel"), out minLvl);

            int maxLvl;
            int.TryParse(Intent.GetStringExtra("maxLevel"), out maxLvl);

            return _itemManager.Search(query, rarity, type, pageSize: PageSize, page: _currentPage);
        }

        private void OnItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            var intent = new Intent(this, typeof (ItemDetailsActivity));
            intent.PutExtra("ItemId", e.Id);
            StartActivity(intent);
        }
    }
}