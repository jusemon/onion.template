namespace Company.Proyect.Infra.IoC.ConfigureServicesExtensions
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

    public static partial class ConfigureServicesExtensions
    {

        public static void ConfigureService(this IServiceCollection services)
        {
            services.AddSingleton<AuthConfig>((provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();
            });
            services.AddSingleton<EmailConfig>((provider) =>
            {
                var configuration = provider.GetService<IConfiguration>();
                return configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
            });
            services.AddTransient(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
