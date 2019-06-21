namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security;
    using Application.Services.Generics.Base;
    using Application.Services.Security;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseApplication<>), typeof(BaseApplication<>));
            services.AddTransient<IUserApplication, UserApplication>();
        }
    }
}
