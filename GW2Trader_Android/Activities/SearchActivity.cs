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
using GW2Trader.Manager;
using TinyIoC;
using GW2Trader.Model;
using GW2Trader_Android.Adapter;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : Activity
    {
        private RelativeLayout _filterLayout;
        private Button _filterToggleButton;
        private Button _searchButton;
        private SearchView _searchView;
        private ListView _resultListView;

        private IItemManager _itemManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Search);

            _searchButton = FindViewById<Button>(Resource.Id.ExecuteSearchButton);
            _searchButton.Click += OnSearchButtonClicked;

            _filterToggleButton = FindViewById<Button>(Resource.Id.FilterToggleButton);
            _filterToggleButton.Click += OnFilterToggleClicked;

            _filterLayout = FindViewById<RelativeLayout>(Resource.Id.FilterLayout);
            _filterLayout.Visibility = ViewStates.Gone;

            _searchView = FindViewById<SearchView>(Resource.Id.SearchView);
            _resultListView = FindViewById<ListView>(Resource.Id.SearchResultListView);

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            string query = _searchView.Query;

            List<Item> items = new List<Item> 
            { 
                new Item { Name = query, Level = 12, Rarity = "Exotic" },
                new Item { Name = query, Level = 12, Rarity = "Exotic" }
            };

            //var items = new string[] {query, query+"2", query+"3"};

            var adapter = new ItemsAdapter(this, items);
            _resultListView.Adapter = adapter;
        }

        private void OnFilterToggleClicked(object sender, EventArgs e)
        {
            if (_filterLayout.Visibility == ViewStates.Gone)
            {
                _filterLayout.Visibility = ViewStates.Visible;
                _filterToggleButton.Text = Resources.GetString(Resource.String.HideFilter);
            }
            else
            {
                _filterLayout.Visibility = ViewStates.Gone;
                _filterToggleButton.Text = Resources.GetString(Resource.String.ExpandFilter);
            }
        }
    }
}