using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2Trader_Windows.Data;
using GW2Trader_Windows.Model;

namespace GW2Trader_Windows.DesignTimeErrorPrevention
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

        public IDbSet<ItemWatchlistModel> ItemWatchlists
        {
            get
            {
                var fakeDbSet = new FakeDbSet<ItemWatchlistModel>
                {
                    new ItemWatchlistModel
                    {
                        Name = "special items",
                        Id = 0,
                        Items = new ObservableCollection<GameItemModel>(_fakeItems.ToList())
                    }
                };
                return fakeDbSet;
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


        public IDbSet<InvestmentModel> Investments
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
    }
}
