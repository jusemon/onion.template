namespace Company.Proyect.Domain.Interfaces.Data
{
    using System.Data;

    public interface IDbFactory
    {
        IDbConnection GetConnection();
    }
}
