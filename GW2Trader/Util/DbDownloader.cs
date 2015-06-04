using GW2Trader.Interface;

namespace GW2Trader.Util
{
    public class DbDownloader : IDbBuilder
    {
        private readonly IFileDownloader _downloader;
        private const string DatabaseUrl = "";

        public DbDownloader(IFileDownloader downloader)
        {
            _downloader = downloader;
        }

        public void BuildItemDatabase(string path)
        {
            _downloader.DownloadFile(DatabaseUrl, path);
        }
    }
}
