using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DAL
{
    public class DeliverySystemContext : DbContext
    {
        public DeliverySystemContext(DbContextOptions optionsBuilder) : base(optionsBuilder)
        {
        }

        public DbSet<Package> Packages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
