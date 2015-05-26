using SQLite.Net.Attributes;

namespace DataLayer.Model
{
    [Table("Icon")]
    public class Icon : IEntity
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Unique, MaxLength(150)]
        public string Url { get; set; }

        [Unique, MaxLength(150)]
        public string FilePath { get; set; }
    }
}
