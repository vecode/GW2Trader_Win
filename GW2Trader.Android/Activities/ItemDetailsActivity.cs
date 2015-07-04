using Android.App;
using Android.OS;
using GW2Trader.Android.Fragments;
using GW2Trader.Manager;
using GW2Trader.Model;
using TinyIoC;

namespace GW2Trader.Android.Activities
{
    [Activity(Label = "ItemDetailsActivity")]
    public class ItemDetailsActivity : Activity
    {
        private Item _item;
        private IItemManager _itemManager;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _itemManager = TinyIoCContainer.Current.Resolve<IItemManager>();

            var itemId = (int) Intent.GetLongExtra("ItemId", 0);
            _item = _itemManager.GetItem(itemId);

            CreateTabs();
        }

        private void CreateTabs()
        {
            ActionBar.NavigationMode = ActionBarNavigationMode.Tabs;
            SetContentView(Resource.Layout.ItemDetailsFragmentContainer);

            AddTab("General", 0, new ItemDetailsFragment(_item));
            AddTab("Buy Orders", 0, new PriceListingFragment(_item, "Buy", "Demand"));
            AddTab("Sell Orders", 0, new PriceListingFragment(_item, "Sell", "Supply"));
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

            tab.TabUnselected +=
                delegate(object sender, ActionBar.TabEventArgs e) { e.FragmentTransaction.Remove(view); };

            ActionBar.AddTab(tab);
        }
    }
}