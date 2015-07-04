using System.Collections.Generic;

namespace GW2Trader.Desktop.Model
{
    public class RarityModel
    {
        public string Name { get; set; }
        public string ColorHexCode { get; set; }

        public static List<RarityModel> Rarities
        {
            get
            {
                return new List<RarityModel>
                {
                    new RarityModel
                    {
                      Name  = "All",
                      ColorHexCode = "#FFF"
                    },
                    new RarityModel
                    {
                        Name = "Junk",
                        ColorHexCode = "#E6E6E6"
                    },
                    new RarityModel
                    {
                        Name = "Basic",
                        ColorHexCode = "#BDBDBD"
                    },
                    new RarityModel
                    {
                        Name = "Fine",
                        ColorHexCode = "#62a4da"
                    },
                    new RarityModel
                    {
                        Name = "Masterwork",
                        ColorHexCode = "#1a9306"
                    },
                    new RarityModel
                    {
                        Name = "Rare",
                        ColorHexCode = "#fcd00b"
                    },
                    new RarityModel
                    {
                        Name = "Exotic",
                        ColorHexCode = "#ffa405"
                    },
                    new RarityModel
                    {
                        Name = "Ascended",
                        ColorHexCode = "#fb3e8d"
                    },
                    new RarityModel
                    {
                        Name = "Legendary",
                        ColorHexCode = "#4c139d"
                    }
                };
            }
        }
    }
}
