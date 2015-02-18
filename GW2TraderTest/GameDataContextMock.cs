using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GW2Trader.Model;
using GW2Trader.Data;
using GW2TPApiWrapper.Entities;

namespace GW2TraderTest
{
    public class GameDataContextMock : IGameDataContext
    {
        private GameDataContext _context = new GameDataContext();

        public GameDataContextMock()
        {
            _context.GameItems.Add(
                new GameItemModel
                {
                    Id = 1,
                    IconUrl = @"http://icon_file_1.png"
                    
                    //Listing = new Listing { Quantity = 100, UnitPrice = 50000 },


                }

                );
        }

        public IDbSet<GameItemModel> GameItems
        {
            get { throw new NotImplementedException(); }
        }

        public IDbSet<Watchlist<InvestmentModel>> Investments
        {
            get { throw new NotImplementedException(); }
        }

        public IDbSet<Watchlist<int>> WatchedItemIds
        {
            get { throw new NotImplementedException(); }
        }
    }
}
