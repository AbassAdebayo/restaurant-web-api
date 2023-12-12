using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.IOC.Extensions
{
    public static class DynamicCollections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddCustomIdentity(config)
                .AddRepositories()
                .AddServices();
            return services;
        }
    }
}
