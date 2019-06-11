namespace Company.Proyect.Infra.IoC.ConfigureServicesExtensions
{
    using Application.Interfaces.Generics.Base;
    using Application.Interfaces.Security;
    using Application.Services.Generics.Base;
    using Application.Services.Security;
    using Microsoft.Extensions.DependencyInjection;

    public static partial class ConfigureServicesExtensions
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseApplication<>), typeof(BaseApplication<>));
            services.AddTransient<IUserApplication, UserApplication>();
        }
    }
}
