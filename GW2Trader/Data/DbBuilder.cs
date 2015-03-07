using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GW2TPApiWrapper.Wrapper;
using System.Data.Entity;
using GW2Trader.Model;
using GW2TPApiWrapper.Entities;
using System.Net;
using GW2Trader.Util;
using System.Collections;

namespace GW2Trader.Data
{
    public class DbBuilder
    {
        private readonly ITradingPostApiWrapper _wrapper;
        private readonly IItemRepository _itemRepository;
        private const int ContextSaveInterval = 100;

        public DbBuilder(ITradingPostApiWrapper wrapper, IItemRepository itemRepository)
        {
            _wrapper = wrapper;
            _itemRepository = itemRepository;
        }

        public void BuildDatabase()
        {
            int[] ids = _wrapper.ItemIds().ToArray();
            List<Item> items = _wrapper.ItemDetails(ids).ToList();

            List<GameItemModel> convertedItems = items.Select(i => ConvertToGameItem(i)).ToList();
            _itemRepository.Add(convertedItems);
            _itemRepository.RebuildItemDictionary();
            _itemRepository.RebuildItemList();
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

        public void LoadIcons()
        {
            List<GameItemModel> allItems = _itemRepository.Items().ToList();
            List<GameItemModel> itemsWithoutIcon = allItems.Where(item => item.IconImageByte == null).ToList();

            // nothing to do here
            if (itemsWithoutIcon.Count == 0)
            {
                return;
            }

            List<string> uniqueIconUrls = itemsWithoutIcon.Select(item => item.IconUrl).Distinct().ToList();

            byte[] image;
            foreach (string url in uniqueIconUrls)
            {
                image = DownloadImage(new Uri(url));
                foreach (GameItemModel item in allItems.Where(i => i.IconUrl.Equals(url)))
                {
                    item.SetIconImageByte(image);
                    _itemRepository.Update(item);
                }
            }
        }

        private byte[] DownloadImage(Uri uri)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadData(uri);
            }
        }

    }
}
