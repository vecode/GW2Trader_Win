using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GW2Trader.Model
{
    public class IconModel
    {
        [Key]
        public int Id { get; set; }

        [MaxLength]
        [Required]
        public byte[] ImageByteArray { get; set; }

        [Required]
        public string UrlHash { get; set; }
    }
}
