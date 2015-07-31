using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text;
using Android.Widget;
using GW2Trader.Android.Filter;
using Android.Support.V7.App;
using Android.Views;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace GW2Trader.Android.Activities
{
    [Activity]
    public class SearchActivity : AppCompatActivity
    {
        private LinearLayout _searchButton;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            InitUI();
        }

        private void InitUI()
        {
            SetContentView(Resource.Layout.Search);

            var toolbar = FindViewById<Toolbar>(Resource.Id.ToolBar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayShowTitleEnabled(false);

            TextView textView = toolbar.FindViewById<TextView>(Resource.Id.Title);
            textView.Text = "Item Search";

            _searchButton = FindViewById<LinearLayout>(Resource.Id.SearchButton);
            _searchButton.Click += OnSearchButtonClicked;

            var minLvl = FindViewById<EditText>(Resource.Id.MinLevel);
            minLvl.SetFilters(new IInputFilter[] {new RangeInputFilter(0, 80), new InputFilterLengthFilter(2)});

            var maxLvl = FindViewById<EditText>(Resource.Id.MaxLevel);
            maxLvl.SetFilters(new IInputFilter[] {new RangeInputFilter(0, 80), new InputFilterLengthFilter(2)});

            var raritySpinner = FindViewById<Spinner>(Resource.Id.RaritySpinner);
            var rarityAdapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.raritiy_array, global::Android.Resource.Layout.SimpleSpinnerItem);
            rarityAdapter.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            raritySpinner.Adapter = rarityAdapter;

            var typeSpinner = FindViewById<Spinner>(Resource.Id.TypeSpinner);
            var typeAdapter = ArrayAdapter.CreateFromResource(
                this, Resource.Array.type_array, global::Android.Resource.Layout.SimpleSpinnerItem);
            typeAdapter.SetDropDownViewResource(global::Android.Resource.Layout.SimpleSpinnerDropDownItem);
            typeSpinner.Adapter = typeAdapter;
        }

        private void OnSearchButtonClicked(object sender, EventArgs e)
        {
            var intent = new Intent(this, typeof (SearchResultActivity));
            intent.PutExtra("Query", FindViewById<SearchView>(Resource.Id.SearchView).Query);
            intent.PutExtra("MinLevel", FindViewById<EditText>(Resource.Id.MinLevel).Text);
            intent.PutExtra("MaxLevel", FindViewById<EditText>(Resource.Id.MaxLevel).Text);
            intent.PutExtra("Rarity", FindViewById<Spinner>(Resource.Id.RaritySpinner).SelectedItem.ToString());
            intent.PutExtra("Type", FindViewById<Spinner>(Resource.Id.TypeSpinner).SelectedItem.ToString());

            StartActivity(intent);
        }

        public override void OnBackPressed()
        {
            Finish();
        }
    }
}