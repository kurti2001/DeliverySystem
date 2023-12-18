using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        public void Configure(EntityTypeBuilder<Package> builder) 
        {
            builder.ToTable("Packages");
            builder.HasKey(x => x.IdPackage);
            builder.Property(x => x.BarcodePackage)
                .IsRequired()
                .HasMaxLength(12);
            builder.HasIndex(x => x.BarcodePackage)
                .IsUnique();
            builder.Property( x => x.SentAddress)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(x => x.DestinationAddress)
                .HasMaxLength(500)
                .IsRequired();
        }
    }

}
