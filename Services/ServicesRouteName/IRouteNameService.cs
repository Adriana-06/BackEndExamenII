using DataAcces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.ServicesRouteName
{
    public interface IRouteNameService
    {
        public Task<List<RouteName>> GetAll();
    }
}
