using DataLayer.Model;

namespace DataLayerTest
{
    internal static class TestDataProvider
    {
        public static Item GetItem()
        {
            return new Item
            {
                Name = "Beginner Sword",
                Rarity = "Rare",
                Type = "Weapon",
                SubType = "Greatsword",
                Level = 0,
                Id = 1
            };

        }

        public static Icon GetIcon()
        {
            return new Icon
            {
                FilePath = "path/to/icon",
                Url = "iconurl.com"
            };

        }
    }
}
