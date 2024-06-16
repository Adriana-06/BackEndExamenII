using DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Services.Extensions.DtoMapping;

namespace Services.ServicesTicket
{
    public interface ITicketService
    {
        public Task<Ticket> CreateTicket(DtoTicket ticket);
        public Task<Ticket> GetTicketById(int ticketId);
        public Task<List<Ticket>> GetAllTickets();
        public Task<List<Ticket>> GetTicketsForRouteOnDate(int routeId, DateTime travelDate);
        public Task<int> GetTicketCountForRouteOnDate(int routeId, DateTime travelDate);
        public Task<int> GetTicketCountBetweenDates(DateTime startDate, DateTime endDate);
        public Task<decimal> GetTotalCollectedAmountBetweenDates(DateTime startDate, DateTime endDate);
        public Task<int> GetPassengerCountForRouteBetweenDates(string source, string target, DateTime startDate, DateTime endDate);
        public Task<decimal> GetTotalCollectedAmountForRouteBetweenDates(string source, string target, DateTime startDate, DateTime endDate);
    }
}
