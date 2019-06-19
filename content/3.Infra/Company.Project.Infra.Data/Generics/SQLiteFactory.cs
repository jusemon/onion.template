namespace Company.Project.Infra.Data.Generics.Base
{
    using Domain.Entities.Config;
    using Domain.Interfaces.Data;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    /// <summary>
    /// SQLite Factory class. 
    /// </summary>
    /// <seealso cref="Company.Project.Domain.Interfaces.Data.IDbFactory" />
    public class SQLiteFactory : IDbFactory
    {
        /// <summary>
        /// The database file
        /// </summary>
        private readonly string dbFile;
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string connectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLiteFactory"/> class.
        /// </summary>
        /// <param name="config">The configuration.</param>
        public SQLiteFactory(DatabaseConfig config)
        {
            this.dbFile = config.DbFile;
            this.connectionString = config.ConnectionString;

            if (!File.Exists(this.dbFile))
            {
                SQLiteConnection.CreateFile(this.dbFile);
            }
        }

        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetConnection()
        {
            var conn = new SQLiteConnection(this.connectionString);
            conn.Open();
            return conn;
        }
    }
}
