using MASA.Framework.Admin.Infrastructure;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensitions
    {
        public static IServiceCollection AddRabbitMQEventBus(this IServiceCollection services, Action<RabbitMQEventBusOptions> configureOptions)
        {
            services.TryAddSingleton<IEventBus, RabbitMQEventBus>();
            services.Configure(configureOptions);

            return services;
        }
    }
}
