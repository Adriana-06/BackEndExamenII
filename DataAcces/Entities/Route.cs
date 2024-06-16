using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Entities
{
    public class Route
    {
        public int Id { get; set; } // Clave primaria para la entidad Route
        public int RouteNameId { get; set; } // Clave foránea que referencia a RouteName
        public string Source { get; set; }
        public string Target { get; set; }
        public decimal Price { get; set; }

        // Propiedad de navegación
        public RouteName? RouteName { get; set; }
    }
}
