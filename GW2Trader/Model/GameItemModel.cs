using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GW2TPApiWrapper.Wrapper;
using GW2TPApiWrapper.Enum;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using GW2TPApiWrapper.Entities;
using System.ComponentModel;
using GW2Trader.MVVM;
using System.Windows;
using System.Windows.Controls;

namespace GW2Trader.Model
{
    public class GameItemModel : ObservableObject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Browsable(false)]
        public int ItemId { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public ItemRarity.Rarity Rarity { get; set; }

        [Required]
        public ItemType.Type Type { get; set; }

        [Required]
        public int RestrictionLevel { get; set; }

        [Required]
        [Browsable(false)]
        public String IconUrl { get; set; }

        [NotMapped]
        [Browsable(false)]
        private ImageSource _iconImageSource { get; set; }

        [NotMapped]
        [Browsable(false)]
        public ImageSource IconImageSource
        {
            get
            {
                if (_iconImageSource == null)
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = new Uri(IconUrl);
                    bitmap.EndInit();
                    _iconImageSource = bitmap;
                }
                return _iconImageSource;
            }
        }

        [NotMapped]
        [Browsable(false)]
        public ItemListing Listing { get; set; }

        [NotMapped]
        private int _buyOrder;

        [NotMapped]
        public int BuyOrder
        {
            get
            {
                return _buyOrder;
            }
            set 
            {
                _buyOrder = value;
                RaisePropertyChanged("BuyOrder");
                RaisePropertyChanged("Margin");
            }
        }

        [NotMapped]
        private int _buyOrderQuantity;

        [NotMapped]
        public int BuyOrderQuantity
        {
            get
            {
                return _buyOrderQuantity;
            }
            set
            {
                _buyOrderQuantity = value;
                RaisePropertyChanged("BuyOrderQuantity");
            }
        }

        [NotMapped]
        private int _sellListing;

        [NotMapped]
        public int SellListing
        {
            get
            {
                return _sellListing;
            }
            set
            {
                _sellListing = value;
                RaisePropertyChanged("SellListing");
                RaisePropertyChanged("Margin");
            }
        }

        [NotMapped]
        private int _sellListingQuantity;

        [NotMapped]
        public int SellListingQuantity
        {
            get
            {
                return _sellListingQuantity;
            }
            set
            {
                _sellListingQuantity = value;
                RaisePropertyChanged("SellListingQuantity");
            }
        }

        [NotMapped]
        private DateTime _commerDataLastUpdated;

        [NotMapped]
        public DateTime CommerceDataLastUpdated
        {
            get
            {
                return _commerDataLastUpdated;
            }
            set
            {
                _commerDataLastUpdated = value;
                RaisePropertyChanged("CommerceDataLastUpdated");
            }
        }

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
        public int ROI
        {
            get
            {
                double result = (1.0 * Margin / BuyOrder) * 100;
                return (int)Math.Round(result);
            }
        }
    }
}
