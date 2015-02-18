using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enums;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;

namespace GW2Trader.Model
{
    public class GameItemModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public Item.ItemRarity Rarity { get; set; }

        [Required]
        public Item.ItemType Type { get; set; }

        [Required]
        public int RestrictionLevel { get; set; }

        [Required]
        public String IconUrl { get; set; }

        [Required]
        private byte[] _image { get; set; }

        [NotMapped]
        private ImageSource _imageSource { get; set; }

        [NotMapped]
        public ImageSource ImageSource
        {
            get
            {
                if (_imageSource == null)
                {
                    BitmapImage img = new BitmapImage();
                    MemoryStream ms = new MemoryStream(_image);
                    img.BeginInit();
                    img.StreamSource = ms;
                    img.EndInit();

                    _imageSource = img as ImageSource;
                }
                return _imageSource;
            }
        }

        [NotMapped]
        public ItemListing Listing { get; set; }

        [NotMapped]
        public ItemPrice Price { get; set; }

        [NotMapped]
        public int Margin
        {
            get
            {
                if (Price != null)
                {
                    // trading post has a 15% fee
                    return (int)Math.Round(Price.Sells.UnitPrice * 0.85) - Price.Buys.UnitPrice;
                }
                else return 0;
            }
        }


        /// <summary>
        /// Calculates the return of investment as percentage based on current prices.
        /// </summary>
        /// <returns>Returns the return of investment based on current prices</returns>
        /// 
        public int ROI()
        {
            double result = (1.0 * Margin / Price.Buys.UnitPrice) * 100;
            return (int)Math.Round(result);
        }
    }
}
