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
        private ListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.SearchResult);

            _listView = FindViewById<ListView>(Resource.Id.SearchResultListView);

            string query = Intent.GetStringExtra("Query");
            string rarity = Intent.GetStringExtra("Rarity");
            string type = Intent.GetStringExtra("Type");

            int minLvl;
            Int32.TryParse(Intent.GetStringExtra("minLevel"), out minLvl);

            int maxLvl;
            Int32.TryParse(Intent.GetStringExtra("maxLevel"), out maxLvl);

            var container = TinyIoCContainer.Current;
            
            var itemManager = container.Resolve<IItemManager>();
            //List<Item> items = itemManager.Search(query, rarity, type, minLevel: minLvl, maxLevel: maxLvl);
            List<Item> items = new List<Item> 
            { 
                new Item { Name = query, Level = 12, Rarity = "Exotic" },
                new Item { Name = query, Level = 12, Rarity = "Exotic" }
            };

            var adapter = new ItemsAdapter(this, items);
            _listView.Adapter = adapter;

        }

        
    }
}