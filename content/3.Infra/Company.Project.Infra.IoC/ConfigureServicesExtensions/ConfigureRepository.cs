namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Data.Contexts;
    using Data.Generics.Base;
    using Domain.Entities.Config;
    using Domain.Interfaces.Generics.Base;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.EntityFrameworkCore;

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
                var configuration = provider.GetRequiredService<IConfiguration>();
                return configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
            });
            services.AddDbContext<SecurityContext>((provider, options) =>
            {
                var dbConfig = provider.GetRequiredService<DatabaseConfig>();
                options.UseSqlite(dbConfig.ConnectionString);
            }, ServiceLifetime.Singleton);
            services.AddSingleton(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }

    }
}
