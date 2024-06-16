using DataAcces.Data;
using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesRouteName
{
    public class RouteNameService : IRouteNameService
    {
        private readonly MyDbContext _context;

        public RouteNameService(MyDbContext context)
        {
            _context = context;
        }

        public async Task<List<RouteName>> GetAll()
        {
            return await _context.RouteNames.ToListAsync();
        }
    }
}
