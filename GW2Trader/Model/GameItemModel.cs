using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Enums;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using GW2TPApiWrapper.Entities;
using System.ComponentModel;

namespace GW2Trader.Model
{
    public class GameItemModel
    {
        [Key]
        [Browsable(false)]
        public int Id { get; set; }

        [Required]
        [Browsable(false)]
        public int ItemId { get; set; }

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
        
        [MaxLength]
        public byte[] IconImageByte { get; set; }

        [NotMapped]
        private ImageSource _iconImageSource { get; set; }

        [NotMapped]
        public ImageSource IconImageSource
        {
            get
            {
                if (_iconImageSource == null)
                {
                    BitmapImage img = new BitmapImage();
                    MemoryStream ms = new MemoryStream(IconImageByte);
                    img.BeginInit();
                    img.StreamSource = ms;
                    img.EndInit();

                    _iconImageSource = img as ImageSource;
                }
                return _iconImageSource;
            }
        }

        [NotMapped]
        public ItemListing Listing { get; set; }

        // TODO add methods for listings average

        [NotMapped]
        public int BuyOrder
        {
            get
            {
                if (Listing != null)
                {
                    return Listing.Buys.OrderByDescending(b => b.UnitPrice)
                        .Select(b => b.UnitPrice)
                        .First();
                }
                else return 0;
            }
        }

        [NotMapped]
        public int BuyOrderQuantity
        {
            get
            {
                if (Listing != null)
                {
                    return Listing.Buys.OrderByDescending(b => b.UnitPrice)
                        .Select(b => b.Quantity)
                        .First();
                }
                else return 0;
            }
        }

        [NotMapped]
        public int SellListing
        {
            get
            {
                if (Listing != null)
                {
                    return Listing.Sells.OrderBy(s => s.UnitPrice)
                        .Select(s => s.UnitPrice)
                        .First();
                }
                else return 0;
            }
        }

        [NotMapped]
        public int SellListingQuantity
        {
            get
            {
                if (Listing != null)
                {
                    return Listing.Sells.OrderBy(s => s.UnitPrice)
                        .Select(s => s.Quantity)
                        .First();
                }
                else return 0;
            }
        }

        [NotMapped]
        public DateTime CommerceDataLastUpdated { get; set; }

        [NotMapped]
        public int Margin
        {
            get
            {
                    // trading post has a 15% fee
                    return (int)Math.Round(SellListing * 0.85) - BuyOrder;
            }
        }

        /// <summary>
        /// Calculates the return of investment as percentage based on current prices.
        /// </summary>
        /// <returns>Returns the return of investment based on current prices</returns>
        /// 
        public int ROI()
        {
            double result = (1.0 * Margin / BuyOrder) * 100;
            return (int)Math.Round(result);
        }
    }
}
