namespace Company.Project.Domain.Entities.Config
{
    /// <summary>
    /// Database Configuration class. 
    /// </summary>
    public class DatabaseConfig
    {
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public string ConnectionString { get; set; } = null!;

        /// <summary>
        /// Gets or sets the database file.
        /// </summary>
        /// <value>
        /// The database file.
        /// </value>
        public string? DbFile { get; set; }
    }
}
