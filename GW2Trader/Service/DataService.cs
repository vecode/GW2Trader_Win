using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Model;
using GW2Trader.Data;

namespace GW2Trader.Service
{
    public class DataService : IDataService
    {
        private readonly IGameDataContextProvider _contextProvider;

        //public DataService(IGameDataContextProvider contextProvider)
        //{
        //    _contextProvider = contextProvider;
        //}

        public IEnumerable<GameItemModel> Items
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<ItemWatchlistModel> ItemWatchlists
        {
            get { throw new NotImplementedException(); }
        }

        public IEnumerable<InvestmentWatchlistModel> InvestmentWatchlists
        {
            get { throw new NotImplementedException(); }
        }

        public void Add(GameItemModel item)
        {
            throw new NotImplementedException();
        }

        public void Add(InvestmentModel investment)
        {
            throw new NotImplementedException();
        }

        public void Add(InvestmentWatchlistModel watchlist)
        {
            throw new NotImplementedException();
        }

        public void Add(ItemWatchlistModel watchlist)
        {
            throw new NotImplementedException();
        }

        public void Update(GameItemModel item)
        {
            throw new NotImplementedException();
        }

        public void Update(InvestmentModel investment)
        {
            throw new NotImplementedException();
        }

        public void Update(InvestmentWatchlistModel watchlist)
        {
            throw new NotImplementedException();
        }

        public void Update(ItemWatchlistModel watchlist)
        {
            throw new NotImplementedException();
        }


        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
