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
    [Activity(Label = "SearchResultActivity")]
    public class SearchResultActivity : Activity
    {
        private PaginatedListAdapter<Item> _itemsAdapter;
        private ListView _listView;
        private List<Item> _displayedItems;

        private Button _nextPageButton;
        private Button _previousPageButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitUI();            

            string query = Intent.GetStringExtra("Query");
            string rarity = Intent.GetStringExtra("Rarity");
            string type = Intent.GetStringExtra("Type");

            int minLvl;
            Int32.TryParse(Intent.GetStringExtra("minLevel"), out minLvl);

            int maxLvl;
            Int32.TryParse(Intent.GetStringExtra("maxLevel"), out maxLvl);

            var container = TinyIoCContainer.Current;
            
            var itemManager = container.Resolve<IItemManager>();
        }

        private void InitUI()
        {
            SetContentView(Resource.Layout.SearchResult);
            _listView = FindViewById<ListView>(Resource.Id.SearchResultListView);

            var _items = Enumerable.Range(1, 34).Select(x => new Item { Name = "name" + x }).ToList();
            _itemsAdapter = new GameItemAdapter(this, _items);
            _listView.Adapter = _itemsAdapter;

            _previousPageButton = FindViewById<Button>(Resource.Id.PreviousButton);
            _previousPageButton.Click += OnPreviousButtonClicked;

            _nextPageButton = FindViewById<Button>(Resource.Id.NextButton);
            _nextPageButton.Click += OnNextButtonClicked;
        }

        private void OnNextButtonClicked(object sender, EventArgs e)
        {
            if (_itemsAdapter.CanMoveForward)
            {
                _itemsAdapter.MoveToNextPage();
            }

            _nextPageButton.Clickable = _itemsAdapter.CanMoveForward;
            _previousPageButton.Clickable = _itemsAdapter.CanMoveBack;
        }

        private void OnPreviousButtonClicked(object sender, EventArgs e)
        {
            if (_itemsAdapter.CanMoveBack)
            {
                _itemsAdapter.MoveToPreviousPage();
            }

            _nextPageButton.Clickable = _itemsAdapter.CanMoveForward;
            _previousPageButton.Clickable = _itemsAdapter.CanMoveBack;
        }
    }
}