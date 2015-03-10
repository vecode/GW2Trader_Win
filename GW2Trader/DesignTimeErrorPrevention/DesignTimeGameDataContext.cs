using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader.Data;
using GW2Trader.Model;

namespace GW2Trader.DesignTimeErrorPrevention
{
    public class DesignTimeGameDataContext : IGameDataContext
    {
        private readonly List<GameItemModel> _fakeItems = new List<GameItemModel>()
        {
            new GameItemModel()
            {
                ItemId = 1,
                IconUrl =
                    @"https://render.guildwars2.com/file/C6110F52DF5AFE0F00A56F9E143E9732176DDDE9/65015.png",
                Name = "Fake Item"

            }
        };

        public IDbSet<GameItemModel> GameItems
        {
            get
            {
                var fakeDbSet = new FakeDbSet<GameItemModel>();
                fakeDbSet.AddRange(_fakeItems);
                return fakeDbSet;
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<InvestmentWatchlistModel> InvestmentWatchlists
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDbSet<ItemIdWatchlistModel> ItemIdWatchlists
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void BulkInsert(IList<Model.GameItemModel> items)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {

        }
    }
}
