using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.ServicesRoute;
using Services.ServicesRoute.Services.ServicesRoute;
using Services.ServicesRouteName;
using Services.ServicesTicket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IRouteNameService, RouteNameService>();
            services.AddScoped<IRouteService, RouteService >();
            services.AddScoped<ITicketService, TicketService>();

            return services;
        }
    }
}
