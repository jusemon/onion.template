namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Data.Generics.Base;
    using Domain.Entities.Config;
    using Domain.Interfaces.Data;
    using Domain.Interfaces.Generics.Base;
    using LightInject;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {
        /// <summary>
        /// Configures the repository.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureRepository(this IServiceContainer services)
        {
            services.Register((provider) =>
            {
                var configuration = provider.GetInstance<IConfiguration>();
                return configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
            }, new PerRequestLifeTime());
            services.Register<IDbFactory, SQLiteFactory>(new PerRequestLifeTime());
            //services.Register(typeof(IBaseRepository<>), typeof(SQLiteBaseRepository<>));
            services.Register(typeof(IBaseRepository<>), typeof(EFSQLiteBaseRepository<>));
        }
    }
}
