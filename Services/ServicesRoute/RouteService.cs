using DataAcces.Data;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesRoute
{
    namespace Services.ServicesRoute
    {
        public class RouteService : IRouteService
        {
            private readonly MyDbContext _context;
            private const int MaxPassengersPerDay = 10;

            public RouteService(MyDbContext context)
            {
                _context = context;
            }

            // Obtener una ruta por ID
            public async Task<Route> GetRouteById(int routeId)
            {
                return await _context.Routes.Include(r => r.RouteName).FirstOrDefaultAsync(r => r.Id == routeId);
            }

            // Obtener todas las rutas
            public async Task<List<Route>> GetAllRoutes()
            {
                return await _context.Routes.Include(r => r.RouteName).ToListAsync();
            }

            // Obtener el precio por origen y destino
            public async Task<decimal> GetPriceBySourceAndTarget(string source, string target)
            {
                if (source == target)
                    return 0;

                var route = await _context.Routes.FirstOrDefaultAsync(r =>
                    (r.Source == source && r.Target == target) ||
                    (r.Source == target && r.Target == source));

                return route?.Price ?? 0;
            }

            // Verificar la capacidad para una ruta en una fecha específica
            public async Task<bool> CheckCapacityForRouteOnDate(int routeId, DateTime travelDate)
            {
                var ticketCount = await _context.Tickets.CountAsync(t => t.RouteId == routeId && t.TravelDate == travelDate);
                return ticketCount < MaxPassengersPerDay;
            }
        }

    }
}