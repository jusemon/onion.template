namespace Company.Proyect.Infra.IoC.ConfigureServicesExtensions
{
    using Data.Generics.Base;
    using Domain.Entities.Config;
    using Domain.Interfaces.Data;
    using Domain.Interfaces.Generics.Base;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigureServicesExtensions
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddSingleton<DatabaseConfig>((provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return configuration.GetSection(nameof(DatabaseConfig)).Get<DatabaseConfig>();
            });
            services.AddTransient<IDbFactory, SQLiteFactory>();
            services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        }
    }
}
