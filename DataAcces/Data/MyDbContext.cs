using DataAcces.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Data
{
    public class MyDbContext : DbContext
    {

        public MyDbContext(DbContextOptions<MyDbContext> options)
    : base(options)
        {

        }
        public DbSet<Route> Routes { get; set; }
        public DbSet<RouteName> RouteNames { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Aquí debes especificar la cadena de conexión a tu base de datos
            optionsBuilder.UseSqlServer("Server=JOSED\\SQLEXPRESS;Database=BackEnd;Trusted_Connection=True; MultipleActiveResultSets=true;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno a muchos entre RouteName y Route
            modelBuilder.Entity<Route>()
                .HasOne(r => r.RouteName)
                .WithMany()
                .HasForeignKey(r => r.RouteNameId);

            // Relación uno a muchos entre Route y Ticket
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Route)
                .WithMany()
                .HasForeignKey(t => t.RouteId);

            modelBuilder.Entity<RouteName>().HasData(
                   new RouteName { Id = 1, Name = "Lugar 1" },
                   new RouteName { Id = 2, Name = "Lugar 2" },
                   new RouteName { Id = 3, Name = "Lugar 3" }
               );

            // Pre-carga de Routes con precios definidos
            modelBuilder.Entity<Route>().HasData(
                // Asumiendo que los IDs de RouteName corresponden a los lugares individuales
                new Route { Id = 1, RouteNameId = 1, Source = "Lugar 1", Target = "Lugar 2", Price = 500 },
                new Route { Id = 2, RouteNameId = 2, Source = "Lugar 2", Target = "Lugar 1", Price = 500 }, // Ruta inversa con el mismo precio

                new Route { Id = 3, RouteNameId = 2, Source = "Lugar 2", Target = "Lugar 3", Price = 1000 },
                new Route { Id = 4, RouteNameId = 3, Source = "Lugar 3", Target = "Lugar 2", Price = 1000 }, // Ruta inversa con el mismo precio

                new Route { Id = 5, RouteNameId = 1, Source = "Lugar 1", Target = "Lugar 3", Price = 1500 },
                new Route { Id = 6, RouteNameId = 3, Source = "Lugar 3", Target = "Lugar 1", Price = 1500 } // Ruta inversa con el mismo precio
            );



        }
    }
}

