namespace Company.Project.Infra.IoC.ConfigureServicesExtensions
{
    using AutoMapper;
    using Application.Interfaces.Security.DTOs;
    using Domain.Entities.Security;
    using Microsoft.Extensions.DependencyInjection;

    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<ActivityDTO, Activity>();
            CreateMap<MenuDto, Menu>();
            CreateMap<PermissionDto, Permission>();
            CreateMap<RoleDto, Role>();
            CreateMap<UserDto, User>();
        }
    }

    /// <summary>
    /// Configure Services Extensions class. 
    /// </summary>
    public static partial class ConfigureServicesExtensions
    {
        /// <summary>
        /// Configures the application.
        /// </summary>
        /// <param name="services">The services.</param>
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MainProfile).Assembly);
        }
    }
}
