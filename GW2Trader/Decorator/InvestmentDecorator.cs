using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GW2Trader.Decorator
{
    public class InvestmentDecorator
    {
        private InvestmentModel _investment;

        public InvestmentDecorator(InvestmentModel investment)
        {
            _investment = investment;
        }

        public int Id
        {
            get { return _investment.Id; }
        }    

        public ImageSource IconImageSource
        {
            get { return _investment.GameItem.IconImageSource; }
        }

        public string Name
        {
            get { return _investment.GameItem.Name; }
        }

        public int BuyPrice
        {
            get { return _investment.GameItem.BuyPrice; }
        }

        public int BuyOrderQuantity
        {
            get { return _investment.GameItem.BuyOrderQuantity; }
        }

        public int SellPrice
        {
            get { return _investment.GameItem.SellPrice; }
        }

        public int SellListingQuantity
        {
            get { return _investment.GameItem.SellListingQuantity; }
        }

        public string Rarity
        {
            get { return _investment.GameItem.Rarity; }
        }

        public bool IsSold
        {
            get { return _investment.IsSold; }
        }

        public int Count
        {
            get { return _investment.Count; }
        }

        public int ExpectedProfitPerUnit
        {
            get { return _investment.PrognosedProfitPerUnit; }
        }

        public int ExpectedTotalProfit
        {
            get { return _investment.PrognosedTotalProfit; }
        }

        public int CurrentProfitPerUnit
        {
            get 
            { 
                return (int)Math.Round(_investment.GameItem.SellPrice * 0.85f - _investment.PurchasePrice); 
            }
        }

        public int CurrentTotalProfit
        {
            get { return CurrentProfitPerUnit * _investment.Count; }
        }

        public int PurchasePrice
        {
            get { return _investment.PurchasePrice; }
        }

        public static InvestmentDecorator Decorate(InvestmentModel investment)
        {
            return new InvestmentDecorator(investment);
        }

        public static IList<InvestmentDecorator> Decorate(IList<InvestmentModel> investments)
        {
            return investments.Select(i => new InvestmentDecorator(i)).ToList();
        }
    }
}
