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
using TinyIoC;
using GW2Trader.Manager;
using GW2Trader.Model;
using GW2Trader_Android.Adapter;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "search result")]
    public class SearchResultActivity : Activity
    {
        private const int PageSize = 10;
        private int _currentPage = 0;

        private IItemManager _itemManager;
        private List<Item> _items;
        private ItemAdapter _itemsAdapter;
        private ListView _listView;
        private TextView _currentIndexTextView;

        private Button _nextPageButton;
        private Button _previousPageButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var container = TinyIoCContainer.Current;
            _itemManager = container.Resolve<IItemManager>();

            _items = Search();

            InitUI();
        }

        private void InitUI()
        {
            SetContentView(Resource.Layout.SearchResult);
            _listView = FindViewById<ListView>(Resource.Id.SearchResultListView);

            _itemsAdapter = new ItemAdapter(this, _items);
            _listView.Adapter = _itemsAdapter;

            _previousPageButton = FindViewById<Button>(Resource.Id.PreviousButton);
            _previousPageButton.Click += OnPreviousButtonClicked;

            _nextPageButton = FindViewById<Button>(Resource.Id.NextButton);
            _nextPageButton.Click += OnNextButtonClicked;
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            _currentPage++;
            List<Item> nextItems = Search();

            if (nextItems.Any())
            {
                _itemsAdapter.GetItems().Clear();
                _itemsAdapter.GetItems().AddRange(nextItems);
                _itemsAdapter.NotifyDataSetChanged();
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
            if (_currentPage > 0)
            {
                _currentPage--;

                List<Item> previousItems = Search();
                if (previousItems.Any())
                {
                    _itemsAdapter.GetItems().Clear();
                    _itemsAdapter.GetItems().AddRange(previousItems);
                    _itemsAdapter.NotifyDataSetChanged();
                }
                _nextPageButton.Clickable = true;
            }
        }

        private List<Item> Search()
        {
            string query = Intent.GetStringExtra("Query");

            string rarity = Intent.GetStringExtra("Rarity");
            if (rarity.ToLower().Equals("all")) { rarity = ""; }

            string type = Intent.GetStringExtra("Type");
            if (type.ToLower().Equals("all")) { type = ""; }

            int minLvl;
            Int32.TryParse(Intent.GetStringExtra("minLevel"), out minLvl);

            int maxLvl;
            Int32.TryParse(Intent.GetStringExtra("maxLevel"), out maxLvl);

            return _itemManager.Search(query, rarity: rarity, type: type, pageSize: PageSize, page: _currentPage);
        }
    }
}