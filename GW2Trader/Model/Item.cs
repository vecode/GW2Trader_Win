using System;

namespace GW2Trader.Model
{
    public class Item : DataLayer.Model.Item
    {
        private const float CommissionFee = 0.15f;

        public int Margin
        {
            get { return (int)Math.Round((SellPrice * CommissionFee) - BuyPrice); }
        }

        public int ReturnOnInvestment
        {
            get
            {
                float roi = Margin / ((float)BuyPrice) * 100;
                return (int)Math.Round(roi);
            }
        }
    }
}
