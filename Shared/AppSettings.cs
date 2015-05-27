using SQLite.Net.Interop;

namespace Shared
{
    public static class AppSettings
    {
        public static void Save() { }

        public static string DatabaseFilePath { get; set; }

        public static ISQLitePlatform SQLitePlatform { get; set; }
    }
}
