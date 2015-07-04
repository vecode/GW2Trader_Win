namespace DataLayer.Model
{
    /// <summary>
    /// Represents an entity of the database.
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// Returns the id of the entity.
        /// </summary>
        int Id { get; set; }
    }
}
