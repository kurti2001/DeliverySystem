using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DAL
{
    public class DeliverySystemContext : IdentityDbContext<User, Role, int>
    {
        public DeliverySystemContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
        }

        public DbSet<Package> Packages { get; set; }
        public DbSet<Area> Areas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
