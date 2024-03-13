using LoggingManagerAdapters.Repositories;
using LoggingManagerCore.Ports.Secundary;

namespace LoggingManagerAPI.Configuration
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
