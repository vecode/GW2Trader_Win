using System.Collections.Generic;
using SQLite.Net.Attributes;
using SQLiteNetExtensions.Attributes;

namespace DataLayer.Model
{
    [Table("InvestmentList")]
    public class InvestmentList : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        [MaxLength(250)]
        public string Description { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [OneToMany]
        public List<Investment> Investments { get; set; }
    }
}
