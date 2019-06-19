namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Domain.Entities.Config;
    using Domain.Interfaces.Email;
    using Domain.Interfaces.Generics.Base;
    using Domain.Services.Email;
    using Domain.Services.Generics.Base;
    using LightInject;
    using Microsoft.Extensions.Configuration;

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {

        /// <summary>
        /// Configures the service.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureService(this IServiceContainer services)
        {
            services.Register((provider) =>
            {
                var configuration = provider.GetInstance<IConfiguration>();
                return configuration.GetSection(nameof(AuthConfig)).Get<AuthConfig>();
            }, new PerRequestLifeTime());
            services.Register((provider) =>
            {
                var configuration = provider.GetInstance<IConfiguration>();
                return configuration.GetSection(nameof(EmailConfig)).Get<EmailConfig>();
            }, new PerRequestLifeTime());
            services.RegisterAssembly(
                typeof(BaseService<>).Assembly, (a, b) => ConfigureServicesHelper.ShouldRegister(a, b, typeof(IBaseService<>)));
            services.Register<IEmailService, EmailService>(new PerRequestLifeTime());
        }
    }
}
