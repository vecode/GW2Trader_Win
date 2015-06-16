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
using GW2Trader_Android.Fragments;

namespace GW2Trader_Android.Activities
{
    [Activity(Label = "ItemDetailsActivity")]
    public class ItemDetailsActivity : Activity
    {
        private IItemManager _itemManager;
        private Item _item;

        protected override void OnCreate(Bundle bundle)
        {          
            base.OnCreate(bundle);            

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();

            int itemId = (int)Intent.GetLongExtra("ItemId", 0);
            _item = _itemManager.GetItem(itemId);

            CreateTabs();
        }

        private void CreateTabs()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.ItemDetailsFragmentContainer);

            AddTab("General", 0, new ItemDetails(_item));
            AddTab("Market", 0, new MarketData());      
        }

        private void AddTab(string name, int iconResourceId, Fragment view)
        {
            var tab = ActionBar.NewTab();
            tab.SetText(name);

            if (iconResourceId != 0)
                tab.SetIcon(iconResourceId);

            tab.TabSelected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                var fragment = FragmentManager.FindFragmentById(Resource.Id.ItemDetailsFragmentContainer);
                if (fragment != null)
                    e.FragmentTransaction.Remove(fragment);
                e.FragmentTransaction.Add(Resource.Id.ItemDetailsFragmentContainer, view);
            };

            tab.TabUnselected += delegate(object sender, ActionBar.TabEventArgs e)
            {
                e.FragmentTransaction.Remove(view);
            };

            ActionBar.AddTab(tab);
        }
    }
}