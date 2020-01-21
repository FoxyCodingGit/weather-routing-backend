using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WeatherRoutingBackend.DataLayer.Models;

namespace WeatherRoutingBackend.DataLayer
{
    public class DatabaseContext: DbContext
    {
        public DbSet<LatLngCoord> LatLongCoords { get; set; }
        public DbSet<ModeOfTransport> ModeOfTransports { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=.;Database=WeatherRouting;Integrated Security=true;");
        }
    }
}
