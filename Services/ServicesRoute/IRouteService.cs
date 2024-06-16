using DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesRoute
{
    public interface IRouteService
    {
        Task<Route> GetRouteById(int routeId);
        Task<List<Route>> GetAllRoutes();
        Task<decimal> GetPriceBySourceAndTarget(string source, string target);
        Task<bool> CheckCapacityForRouteOnDate(int routeId, DateTime travelDate);
    }
}
