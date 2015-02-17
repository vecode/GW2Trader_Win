using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GW2Trader.Model
{
    public class Investment
    {
        [Key]
        int Id { get; set; }

        [Required]
        public bool IsSold { get; set; }

        [Required]
        [DataType(DataType.Date)]
        DateTime PurchaseDate { get; set; }

        [Required]
        [ForeignKey("Id")]
        public int ItemId { get; set; }

        [Required]
        public int PurchasePrice { get; set; }

        [Required]
        public int Count { get; set; }

        public int? DesiredSellPrice { get; set; }

        public int? SoldFor { get; set; }

        public Investment(int itemId)
        {
            ItemId = itemId;
            IsSold = false;
        }

        public int ProfitPerUnit()
        {
            if (SoldFor == null)
            {
                if (DesiredSellPrice != null)
                {
                    return (int)(DesiredSellPrice * 0.85) - PurchasePrice;
                }
                else { return 0; }
            }
            else
            {
                return (int)(SoldFor * 0.85) - PurchasePrice;
            }
        }

        public int TotalProfit()
        {
            return ProfitPerUnit() * Count;
        }
    }
}
