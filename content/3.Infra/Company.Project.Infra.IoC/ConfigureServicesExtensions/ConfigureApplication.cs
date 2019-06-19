namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using Application.Interfaces.Generics.Base;
    using Application.Services.Generics.Base;
    using LightInject;

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureApplication(this IServiceContainer services)
        {
            services.RegisterAssembly(
                typeof(BaseApplication<>).Assembly, (a, b) => ConfigureServicesHelper.ShouldRegister(a, b, typeof(IBaseApplication<>)));
        }
    }
}
