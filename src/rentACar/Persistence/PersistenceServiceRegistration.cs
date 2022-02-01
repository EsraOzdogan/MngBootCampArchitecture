using Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class PersistenceServiceRegistration  //Entension oldugu icin static yaptim
    {
        public static IServiceCollection AddServicePersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer
                                                    (configuration.GetConnectionString("rentACarConnectionString")));
            services.AddScoped<IBrandRepository, BrandRapository>();

            return services;
        }
    }
}
