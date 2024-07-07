using Blue.Challenge.Infra.Interfaces;
using Blue.Challenge.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Blue.Challenge.Infra.Extensions
{
    public static class DependencyInjectionExtensions
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
    }
}
