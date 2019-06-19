namespace Company.Project.Domain.Interfaces.Data
{
    using System.Data;

    /// <summary>
    /// Database Factory interface. 
    /// </summary>
    public interface IDbFactory
    {
        /// <summary>
        /// Gets the connection.
        /// </summary>
        /// <returns></returns>
        IDbConnection GetConnection();
    }
}
