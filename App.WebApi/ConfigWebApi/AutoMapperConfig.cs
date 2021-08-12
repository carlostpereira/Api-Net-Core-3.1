using App.Domain.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace App.WebApi.ConfigWebApi
{
    public static class AutoMapperConfig
    {
        public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(UserProfile),
                typeof(ProviderProfile)
            );

            return services;
        }
    }
}
