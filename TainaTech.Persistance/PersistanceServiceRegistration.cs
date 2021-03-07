using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TainaTech.Application.Contracts.Persistance;
using TainaTech.Persistance.Repositories;

namespace TainaTech.Persistance
{
    public static class PersistanceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PeopleDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("PeopleDbConnectionString"),
                        // resilient connections
                        sqlServerOptionsAction: sqlOptions =>
                        {
                            sqlOptions.EnableRetryOnFailure(
                                maxRetryCount: 5,
                                maxRetryDelay: TimeSpan.FromSeconds(30),
                                errorNumbersToAdd: null);
                        });
            });

            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
