using GW2Trader.Data;
using GW2Trader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Util
{
    public class IconBackgroundDownloader
    {
        private IItemRepository _repository;
        private const int ContextSaveInterval = 5;

        public IconBackgroundDownloader(IItemRepository repository)
        {
            _repository = repository;
        }

        public void LoadIcons()
        {
            Logger.Instance.AddLog("loading icons..");
            List<GameItemModel> allItems = _repository.Items().ToList().Take(5).ToList();
            List<GameItemModel> itemsWithoutIcon = allItems.Where(item => item.IconImageByte == null).ToList();
            Logger.Instance.AddLog(itemsWithoutIcon.Count + " items without icon");

            // nothing to do here
            if (itemsWithoutIcon.Count == 0)
            {
                return;
            }

            List<string> uniqueIconUrls = itemsWithoutIcon.Select(item => item.IconUrl).Distinct().ToList();

            Logger.Instance.AddLog(uniqueIconUrls.Count + " unique urls");

            byte[] image;
            foreach (string url in uniqueIconUrls)
            {
                image = DownloadImage(new Uri(url));
                Logger.Instance.AddLog("downloaded " + url);
                foreach (GameItemModel item in _repository.Items().Where(i => i.IconUrl.Equals(url))) 
                {
                    item.IconImageByte = image;
                    _repository.Update(item);

                    Logger.Instance.AddLog("saved " + url + " for entity");
                }

            }
            //return;
            //int count = 0;
            //foreach (GameItemModel item in itemsWithoutIcon)
            //{
            //    item.IconImageByte = iconDictionary[item.IconUrl];
            //    count++;
            //    if (count == ContextSaveInterval)
            //    {
            //        _context.Save();
            //        count = 0;
            //    }
            //}
            //_context.Save();
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
