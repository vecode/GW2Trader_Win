using GW2Trader.Model;
using GW2Trader.MVVM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Decorator
{
    public class InvestmentWatchlistDecorator : ObservableObject
    {
        private InvestmentWatchlistModel _investmentWatchlist;
        private ObservableCollection<InvestmentDecorator> _items;

        public InvestmentWatchlistDecorator(InvestmentWatchlistModel investmentWatchlist)
        {
            _investmentWatchlist = investmentWatchlist;
            _items = new ObservableCollection<InvestmentDecorator>(
                InvestmentDecorator.Decorate(investmentWatchlist.Items));
        }

        public string Name
        {
            get { return _investmentWatchlist.Name; }
            set 
            {
                _investmentWatchlist.Name = value;
                RaisePropertyChanged("Name");
            }        
        }

        public string Description
        {
            get { return _investmentWatchlist.Description; }
            set
            {
                _investmentWatchlist.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public int Id
        {
            get { return _investmentWatchlist.Id; }
        }

        public ObservableCollection<InvestmentDecorator> Items
        {
            get { return _items; }
        }


        public static InvestmentWatchlistDecorator Decorate(InvestmentWatchlistModel investmentWatchlist)
        {
            return new InvestmentWatchlistDecorator(investmentWatchlist);
        }

        public static IList<InvestmentWatchlistDecorator> Decorate(IList<InvestmentWatchlistModel> investmentWatchlists)
        {
            return investmentWatchlists.Select(i => new InvestmentWatchlistDecorator(i)).ToList();
        }
    }
}
