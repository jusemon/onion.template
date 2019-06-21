namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Domain.Entities.Config;
    using Domain.Interfaces.Email;
    using Domain.Interfaces.Generics.Base;
    using Domain.Interfaces.Security.User;
    using Domain.Services.Email;
    using Domain.Services.Generics.Base;
    using Domain.Services.Security.User;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {

        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddSingleton((provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();
            });
            services.AddSingleton((provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
            });
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
