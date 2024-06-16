using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Entities
{
    public class Ticket
    {
        public int Id { get; set; } // Clave primaria para la entidad Ticket
        public int RouteId { get; set; } // Clave foránea que referencia a Route
        public DateTime TravelDate { get; set; } // Fecha del viaje

        public decimal Price { get; set; }

        // Propiedad de navegación
        public Route? Route { get; set; }
    }
}
