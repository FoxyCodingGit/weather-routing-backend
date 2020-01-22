using Microsoft.EntityFrameworkCore;
using WeatherRoutingBackend.DataLayer.Models;

namespace WeatherRoutingBackend.DataLayer
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Route> Routes { get; set; }
        public DbSet<User> Users { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options) { }
    }
}
