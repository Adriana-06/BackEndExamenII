using DataAcces.Data;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using Services.ServicesRoute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Extensions.DtoMapping;

namespace Services.ServicesTicket
{
    public class TicketService : ITicketService
    {
        private readonly MyDbContext _context;
        private readonly IRouteService _routeService;

        public TicketService(MyDbContext context, IRouteService routeService)
        {
            _context = context;
            _routeService = routeService;
        }

        // Crear un nuevo tiquete
        public async Task<Ticket> CreateTicket(DtoTicket dtoTicket)
        {
            // Verificar si la ruta existe
            var route = await _routeService.GetRouteById(dtoTicket.RouteId);
            if (route == null)
            {
                throw new Exception("Route not found");
            }

            // Obtener el precio para la ruta
            decimal price = await _routeService.GetPriceBySourceAndTarget(route.Source, route.Target);
            if (price == 0)
            {
                throw new Exception("Price could not be determined for the selected route");
            }

            // Verificar capacidad
            if (!await _routeService.CheckCapacityForRouteOnDate(route.Id, dtoTicket.TravelDate))
            {
                throw new Exception("The route has reached the maximum capacity for the selected date");
            }

            // Crear el ticket
            var ticket = new Ticket
            {
                RouteId = dtoTicket.RouteId,
                TravelDate = dtoTicket.TravelDate,
                Price = price // Asignar el precio obtenido
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }




        // Obtener un tiquete por ID
        public async Task<Ticket> GetTicketById(int ticketId)
        {
            return await _context.Tickets.Include(t => t.Route).FirstOrDefaultAsync(t => t.Id == ticketId);
        }

        // Obtener todos los tiquetes
        public async Task<List<Ticket>> GetAllTickets()
        {
            return await _context.Tickets.Include(t => t.Route).ToListAsync();
        }

        // Obtener tiquetes para una ruta en una fecha específica
        public async Task<List<Ticket>> GetTicketsForRouteOnDate(int routeId, DateTime travelDate)
        {
            return await _context.Tickets
                .Include(t => t.Route)
                .Where(t => t.RouteId == routeId && t.TravelDate == travelDate)
                .ToListAsync();
        }

        // Obtener el conteo de tiquetes para una ruta en una fecha específica
        public async Task<int> GetTicketCountForRouteOnDate(int routeId, DateTime travelDate)
        {
            return await _context.Tickets
                .CountAsync(t => t.RouteId == routeId && t.TravelDate == travelDate);
        }

        // Obtener el conteo de tiquetes entre dos fechas
        public async Task<int> GetTicketCountBetweenDates(DateTime startDate, DateTime endDate)
        {
            return await _context.Tickets
                .CountAsync(t => t.TravelDate >= startDate && t.TravelDate <= endDate);
        }

        // Obtener el monto total recolectado entre dos fechas
        public async Task<decimal> GetTotalCollectedAmountBetweenDates(DateTime startDate, DateTime endDate)
        {
            var tickets = await _context.Tickets
                .Where(t => t.TravelDate >= startDate && t.TravelDate <= endDate)
                .Include(t => t.Route)
                .ToListAsync();

            return tickets.Sum(t => t.Route.Price);
        }

        // Obtener el número total de pasajeros para una ruta entre dos fechas
        public async Task<int> GetPassengerCountForRouteBetweenDates(string source, string target, DateTime startDate, DateTime endDate)
        {
            var routes = await _context.Routes
                .Where(r => (r.Source == source && r.Target == target) || (r.Source == target && r.Target == source))
                .Select(r => r.Id)
                .ToListAsync();

            return await _context.Tickets
                .CountAsync(t => routes.Contains(t.RouteId) && t.TravelDate >= startDate && t.TravelDate <= endDate);
        }

        // Obtener el monto total recolectado para una ruta entre dos fechas
        public async Task<decimal> GetTotalCollectedAmountForRouteBetweenDates(string source, string target, DateTime startDate, DateTime endDate)
        {
            var routes = await _context.Routes
                .Where(r => (r.Source == source && r.Target == target) || (r.Source == target && r.Target == source))
                .Select(r => r.Id)
                .ToListAsync();

            var tickets = await _context.Tickets
                .Where(t => routes.Contains(t.RouteId) && t.TravelDate >= startDate && t.TravelDate <= endDate)
                .Include(t => t.Route)
                .ToListAsync();

            return tickets.Sum(t => t.Route.Price);
        }
    }
}
