using DataLayer.Model;
using GW2Trader.Manager;
using Xunit;

namespace GW2TraderTest
{
    public class IconStoreTest
    {
        [Fact]
        public void FileNameShouldBeSplittedFromUrl()
        {
            string url = "https://render.guildwars2.com/file/C6110F52DF5AFE0F00A56F9E143E9732176DDDE9/65015.png";
            string actualFileName = IconStore.ExtractFileName(url);

            Assert.Equal("65015.png", actualFileName);
        }

        [Fact]
        public void TestGetIconPath()
        {
            string placeholderFile = "resources/placeholder.png";
            IconStore iconStore = new IconStore("images", placeholderFile, null, null);

            // item with valid iconFileName
            Item item = new Item {IconUrl = "https://render.guildwars2.com/file/C6110F52DF5AFE0F00A56F9E143E9732176DDDE9/65015.png"};
            Assert.Equal("images/65015.png", iconStore.GetIconPath(item));

            // item without iconFileName
            item.IconUrl = null;
            Assert.Equal(placeholderFile, iconStore.GetIconPath(item));
        }
    }
}
