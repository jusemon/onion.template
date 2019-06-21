namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Data.Generics.Base;
    using Domain.Entities.Config;
    using Domain.Interfaces.Data;
    using Domain.Interfaces.Generics.Base;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {

        /// <summary>
        /// Configures the repository.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddSingleton((provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
            });
            services.AddSingleton<IDbFactory, SQLiteFactory>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(SQLiteBaseRepository<>)); // typeof(EFSQLiteBaseRepository<>);
        }

    }
}
