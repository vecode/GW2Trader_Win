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
using GW2Trader_Android.Filter;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "SearchActivity")]
    public class SearchActivity : Activity
    {
        private Button _searchButton;
        private SearchView _searchView;
        private ListView _resultListView;

        private IItemManager _itemManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitUI();


            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();
        }

        private void InitUI()
        {
            SetContentView(Resource.Layout.Search);

            _searchButton = FindViewById<Button>(Resource.Id.ExecuteSearchButton);
            _searchButton.Click += OnSearchButtonClicked;

            _searchView = FindViewById<SearchView>(Resource.Id.SearchView);

            EditText minLvl = FindViewById<EditText>(Resource.Id.MinLevel);
            minLvl.SetFilters(new Android.Text.IInputFilter[] { new RangeInputFilter(0, 80) });

            EditText maxLvl = FindViewById<EditText>(Resource.Id.MaxLevel);
            maxLvl.SetFilters(new Android.Text.IInputFilter[] { new RangeInputFilter(0, 80) });

            Spinner raritySpinner = FindViewById<Spinner>(Resource.Id.RaritySpinner);
            var rarityAdapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.raritiy_array, Android.Resource.Layout.SimpleSpinnerItem);
            rarityAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            raritySpinner.Adapter = rarityAdapter;

            Spinner typeSpinner = FindViewById<Spinner>(Resource.Id.TypeSpinner);
            var typeAdapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.type_array, Android.Resource.Layout.SimpleSpinnerItem);
            typeAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
            typeSpinner.Adapter = typeAdapter;
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

            //var adapter = new ItemsAdapter(this, items);
            //_resultListView.Adapter = adapter;
        }
    }
}