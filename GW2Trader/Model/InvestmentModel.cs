﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GW2Trader.Model
{
    public class InvestmentModel
    {
        [NotMapped]
        private const float SalesCommission = 0.15f;

        [Key]
        public int Id { get; set; }

        [Required]
        public bool IsSold { get; set; }

        [Required]
        [DataType(DataType.Date)]
        private DateTime PurchaseDate { get; set; }

        [Required]
        public int PurchasePrice { get; set; }

        [Required]
        public int Count { get; set; }

        public int? DesiredSellPrice { get; set; }
        public int? SoldFor { get; set; }

        // navigation property
        [Browsable(false)]
        public virtual ICollection<InvestmentWatchlistModel> InvestmentLists { get; set; }

        // navigation property
        [Browsable(false)]
        public virtual GameItemModel GameItem { get; set; }

        [NotMapped]
        public int PrognosedProfitPerUnit
        {
            get
            {
                if (DesiredSellPrice != null)
                {
                    return (int)(DesiredSellPrice * (1 - SalesCommission)) - PurchasePrice;
                }
                return 0;
            }
        }

        [NotMapped]
        public int PrognosedTotalProfit
        {
            get { return PrognosedProfitPerUnit * Count; }
        }

        [NotMapped]
        public int ActualProfitPerUnit
        {
            get
            {
                if (SoldFor != null)
                {
                    return (int)(SoldFor * (1 - SalesCommission)) - PurchasePrice;
                }
                return 0;
            }
        }

        [NotMapped]
        public int ActualTotalProfit
        {
            get { return ActualProfitPerUnit * Count; }
        }
    }
}