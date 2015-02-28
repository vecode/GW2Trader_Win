using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using System.Data.Entity;
using GW2Trader.Model;
using GW2TPApiWrapper.Entities;


namespace GW2Trader.Data
{
    public class DbBuilder
    {
        private readonly ITradingPostApiWrapper _wrapper;
        private readonly IGameDataContext _context;

        public DbBuilder(ITradingPostApiWrapper wrapper, IGameDataContext context)
        {
            _wrapper = wrapper;
            _context = context;
        }

        public void BuildDatabase()
        {
            int[] ids = _wrapper.ItemIds();

            List<Item> items = _wrapper.ItemDetails(ids).ToList();
            
            items.ForEach(i => 
                _context.GameItems.Add(ConvertToGameItem(i)));
            _context.Save();
        }

        private static GameItemModel ConvertToGameItem(Item item)
        {
            GameItemModel itemModel = new GameItemModel
            {
                ItemId = item.Id,
                IconUrl = item.IconUrl,
                Name = item.Name,
                Rarity = item.Rarity,
                RestrictionLevel = item.Level,
                Type = item.Type,
                CommerceDataLastUpdated = DateTime.Now
            };
            return itemModel;
        }
    }
}
