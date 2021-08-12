using App.Domain.Commands.Handlers;
using App.Domain.Repositories;
using App.Infrastructure.Contexts;
using App.Infrastructure.Repositories;
using App.Infrastructure.Transactions;
using App.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace App.WebApi.ConfigWebApi
{
    public static class DependenceResolverConfig
    {
        public static IServiceCollection DependenceResolver(this IServiceCollection services)
        {

            services.AddDbContextPool<ApplicationDataContext>(opt => opt.UseSqlServer(Runtime.ConnectionString));

            services.AddScoped<ApplicationDataContext, ApplicationDataContext>();
            services.AddTransient<IUow, Uow>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<UserCommandHandler, UserCommandHandler>();

            services.AddTransient<IProviderRepository, ProviderRepository>();
            services.AddTransient<ProviderCommandHandler, ProviderCommandHandler>();

            return services;
        }
    }
}
