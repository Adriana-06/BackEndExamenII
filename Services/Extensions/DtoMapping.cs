using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Extensions
{
    public class DtoMapping
    {


        public class DtoTicket
        {
            public int RouteId { get; set; }

            public DateTime TravelDate { get; set; }
        }
    }
}
    
