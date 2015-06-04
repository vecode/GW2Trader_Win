namespace GW2Trader.Interface
{
    // TODO interface obsolete?
    public interface IFileDownloader
    {
        void DownloadFile(string url, string fileName);
        void DownloadFileAsync(string url, string fileName);
    }
}
