using LoggingManagerAdapters.Repositories;
using LoggingManagerAdapters.Services;
using LoggingManagerCore.Ports.Primary;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAPI.Configuration
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISystemRepository, SystemRepository>();

            //Services
            services.AddScoped<ISystemService, SystemService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }
    }
}
