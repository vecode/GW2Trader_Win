using System;
using System.Collections.Generic;

namespace GW2Trader.Model
{
    public class Item : DataLayer.Model.Item
    {
        private const float CommissionFee = 0.15f;

        public Item() { }

        public Item(DataLayer.Model.Item item)
        {
            BuyPrice = item.BuyPrice;
            SellPrice = item.SellPrice;
            Name = item.Name;
            Rarity = item.Rarity;
            CommerceDataLastUpdated = item.CommerceDataLastUpdated;
            Demand = item.Demand;
            IconUrl = item.IconUrl;
            Id = item.Id;
            Level = item.Level;
            PreviousBuyPrice = item.PreviousBuyPrice;
            PreviousDemand = item.PreviousDemand;
            PreviousSellPrice = item.PreviousSellPrice;
            PreviousSupply = item.PreviousSupply;
            Type = item.Type;
            SubType = item.SubType;
            Supply = item.Supply;
        }        

        public int Margin
        {
            get { return (int)Math.Round((SellPrice * (1 - CommissionFee)) - BuyPrice); }
        }

        public int ReturnOnInvestment
        {
            get
            {
                float roi = Margin / ((float)BuyPrice) * 100;
                return (int)Math.Round(roi);
            }
        }

        public List<PriceListing> SellOrders { get; set; }

        public List<PriceListing> BuyOrders { get; set; }
    }
}
