using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DAL.Entities;

namespace DAL.DbConfig
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role>builder)
        {
            builder.ToTable("Roles");
            builder.Property(x => x.Id)
                   .ValueGeneratedNever();
            builder.HasData(new Role
            {
                Id = 1,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "1"
            });
            builder.HasData(new Role
            { 
                Id = 2,
                Name = "Recepsionist",
                NormalizedName = "RECEPSIONIST",
                ConcurrencyStamp = "2"
            });
            builder.HasData(new Role
            {
                Id = 3,
                Name = "Transporter",
                NormalizedName = "TRANSPORTER",
                ConcurrencyStamp = "3"
            });
            builder.HasData(new Role    
            {
                Id = 4,
                Name = "Delivery",
                NormalizedName = "DELIVERY",
                ConcurrencyStamp = "4"
            });
            builder.HasData(new Role
            {
                Id = 5,
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "5"
            });
        }
    }
}
