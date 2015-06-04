using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using GW2Trader.Interface;

namespace GW2Trader.Manager
{
    // TODO move to respective GUI project
    public class IconStore : IIconStore
    {
        private readonly string _fileDirectory;
        private readonly string _placeholderImagePath;
        private readonly IFileAccess _fileAccess;
        private readonly IFileDownloader _downloader;

        public IconStore(string directory, string placeholderImagePath,
            IFileAccess fileAccess, IFileDownloader downloader)
        {
            _fileDirectory = directory;
            _placeholderImagePath = placeholderImagePath;
            _fileAccess = fileAccess;
            _downloader = downloader;
        }

        public string GetIconPath(DataLayer.Model.Item item)
        {
            if (string.IsNullOrEmpty(item.IconUrl)) { return _placeholderImagePath; }

            string absolutePath = Path.Combine(_fileDirectory, ExtractFileName(item.IconUrl));

            if (!_fileAccess.FileExists(absolutePath))
            {
                try
                {
                    _downloader.DownloadFile(item.IconUrl, absolutePath);
                }
                catch (Exception)
                {
                    return _placeholderImagePath;
                }
            }
            return absolutePath;
        }

        public void DownloadMissingIcons(List<DataLayer.Model.Item> items, CancellationToken cancellationToken)
        {
            var iconUrls = items.Select(i => i.IconUrl).Distinct();
            foreach (string iconUrl in iconUrls)
            {
                if (cancellationToken.IsCancellationRequested) { return; }

                string fileName = ExtractFileName(iconUrl);
                if (!_fileAccess.FileExists(fileName))
                {
                    string path = Path.Combine(_fileDirectory, fileName);
                    _downloader.DownloadFile(iconUrl, path);
                }
            }
        }

        public static string ExtractFileName(string url)
        {
            return Path.GetFileName(url);
        }
    }
}
