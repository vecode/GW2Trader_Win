using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GW2Trader.Model
{
    public class InvestmentModel
    {
        [NotMapped]
        private readonly float _salesCommission = 0.15f;

        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsSold { get; set; }

        [Required]
        [DataType(DataType.Date)]
        DateTime PurchaseDate { get; set; }
       
        public int ItemId { get; set; }
        [ForeignKey("ItemId")]
        public virtual GameItemModel GameItem { get; set; }

        [Required]
        public int PurchasePrice { get; set; }

        [Required]
        public int Count { get; set; }

        public int? DesiredSellPrice { get; set; }

        public int? SoldFor { get; set; }

        public InvestmentModel()
        {
            IsSold = false;
        }

        public InvestmentModel(int itemId)
        {
            ItemId = itemId;
            IsSold = false;
        }

        public int PrognosedProfitPerUnit()
        {
            if (DesiredSellPrice != null)
            {
                return (int)(DesiredSellPrice * (1 - _salesCommission)) - PurchasePrice;
            }
            else return 0;
        }

        public int PrognosedTotalProfit()
        {
            return PrognosedProfitPerUnit() * Count;
        }

        public int ActualProfitPerUnit()
        {
            if (SoldFor != null)
            {
                return (int)(SoldFor * (1 - _salesCommission)) - PurchasePrice;
            }
            else return 0;
        }

        public int ActualTotalProfit()
        {
            return ActualProfitPerUnit() * Count;
        }
    }
}
