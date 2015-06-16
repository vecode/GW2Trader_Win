using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using GW2Trader.Model;

namespace GW2Trader_Android.Fragments
{
    public class ItemDetails : Fragment
    {
        private Item _item;

        public ItemDetails(Item item)
            :base()
        {
            _item = item;
        }

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);            
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = inflater.Inflate(Resource.Layout.ItemDetails, container, false);
            var icon = view.FindViewById<ImageView>(Resource.Id.Icon);
            icon.SetImageResource(Resource.Drawable.placeholder);

            var name = view.FindViewById<TextView>(Resource.Id.Name);
            name.Text = _item.Name;

            var type = view.FindViewById<TextView>(Resource.Id.Type);
            type.Text = _item.Type;

            var subType = view.FindViewById<TextView>(Resource.Id.SubType);
            subType.Text = _item.SubType;

            var rarity = view.FindViewById<TextView>(Resource.Id.Rarity);
            rarity.Text = _item.Rarity;

            return view;
        }
    }
}