using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GW2TPApiWrapper.Entities;
using GW2TPApiWrapper.Enum;
using GW2Trader.MVVM;

namespace GW2Trader.Model
{
    public class GameItemModel : ObservableObject
    {
        [NotMapped]
        private int _buyPrice;
        [NotMapped]
        private int _buyOrderQuantity;
        [NotMapped]
        private DateTime _commerceDataLastUpdated;
        [NotMapped]
        private int _sellPrice;
        [NotMapped]
        private int _sellListingQuantity;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Browsable(false)]
        public int ItemId { get; set; }

        // navigation property
        [Browsable(false)]
        public virtual ICollection<ItemWatchlistModel> Watchlists { get; set; }

         // navigation property
        [Browsable(false)]
        public virtual ICollection<InvestmentModel> Investments { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public string Rarity { get; set; }

        [Required]
        public string Type { get; set; }

        public string SubType { get; set; }

        [Required]
        public int RestrictionLevel { get; set; }

        [Required]
        [Browsable(false)]
        public String IconUrl { get; set; }

        [NotMapped]
        [Browsable(false)]
        private ImageSource _iconImageSource;

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
        private ItemListing _listing;


        [NotMapped]
        //[Browsable(false)]
        public ItemListing Listing
        {
            get { return _listing; }
            set
            {
                _listing = value;
                RaisePropertyChanged("Listing");
            }
        }

        [NotMapped]
        public int BuyPrice
        {
            get { return _buyPrice; }
            set
            {
                _buyPrice = value;
                RaisePropertyChanged("BuyPrice");
                RaisePropertyChanged("Margin");
                RaisePropertyChanged("ROI");
            }
        }

        [NotMapped]
        public int BuyOrderQuantity
        {
            get { return _buyOrderQuantity; }
            set
            {
                _buyOrderQuantity = value;
                RaisePropertyChanged("BuyOrderQuantity");
            }
        }

        [NotMapped]
        public int SellPrice
        {
            get { return _sellPrice; }
            set
            {
                _sellPrice = value;
                RaisePropertyChanged("SellPrice");
                RaisePropertyChanged("Margin");
                RaisePropertyChanged("ROI");
            }
        }

        [NotMapped]
        public int SellListingQuantity
        {
            get { return _sellListingQuantity; }
            set
            {
                _sellListingQuantity = value;
                RaisePropertyChanged("SellListingQuantity");
            }
        }

        [NotMapped]
        public DateTime CommerceDataLastUpdated
        {
            get { return _commerceDataLastUpdated; }
            set
            {
                _commerceDataLastUpdated = value;
                RaisePropertyChanged("CommerceDataLastUpdated");
            }
        }

        [NotMapped]
        public int Margin
        {
            get
            {
                // trading post has a 15% fee
                return (int)Math.Round((SellPrice * 0.85) - BuyPrice);
            }
        }

        /// <summary>
        ///     Calculates the return on investment as percentage based on current prices.
        /// </summary>
        /// <returns>Returns the return on investment based on current prices</returns>
        ///
        [NotMapped]
        public int ROI
        {
            get
            {
                var result = (1.0 * Margin / BuyPrice) * 100;
                return (int)Math.Round(result);
            }
        }

        // TODO remove this later
        public GameItemModel()
        {
            Listing = new ItemListing
            {
                Buys = new Listing[0],
                Sells = new Listing[0]
            };
        }
    }
}