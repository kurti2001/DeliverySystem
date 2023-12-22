using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class ShipmentConfiguration:IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder) 
        {
            builder.ToTable("Shipments");
            builder.HasKey(x => x.ShipmentId);

            builder.HasOne(x => x.Package)
                   .WithMany(p => p.Shipments)
                   .HasForeignKey(x => x.IdPackage)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(x => x.PostalOffice)
                   .WithMany(po => po.Shipments)
                   .HasForeignKey(x => x.PostalOfficeId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.Property(x => x.ArrivalDate)
                   .IsRequired();
            builder.Property(x=> x.DepartureDate)
                   .IsRequired();
        }
    }
}
