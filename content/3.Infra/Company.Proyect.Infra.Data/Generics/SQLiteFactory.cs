namespace Company.Proyect.Infra.Data.Generics.Base
{
    using Domain.Entities.Config;
    using Domain.Interfaces.Data;
    using System.Data;
    using System.Data.SQLite;
    using System.IO;

    public class SQLiteFactory : IDbFactory
    {
        private readonly string dbFile;
        private readonly string connectionString;

        public SQLiteFactory(DatabaseConfig config)
        {
            this.dbFile = config.DbFile;
            this.connectionString = config.ConnectionString;

            if (!File.Exists(this.dbFile))
            {
                SQLiteConnection.CreateFile(this.dbFile);
            }
        }

        public IDbConnection GetConnection()
        {
            var conn = new SQLiteConnection(this.connectionString);
            conn.Open();
            return conn;
        }
    }
}
