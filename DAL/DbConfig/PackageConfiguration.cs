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
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(500);
            builder.Property(x => x.SenderInformation)
                   .IsRequired();
            builder.Property(x => x.Email)
                   .IsRequired();
            builder.Property(x => x.DestinationAddress)
                   .IsRequired();
            builder.Property(x => x.DestinationZipCode)
                   .IsRequired();
            builder.Property(x => x.SentAddress)
                   .IsRequired();
            builder.Property(x => x.SentZipCode)
                   .IsRequired();

            builder.Property(x => x.Status)
                  .IsRequired()
                  .HasMaxLength(50) 
                  .HasDefaultValue(PackageStatus.Created);
        }
    }

}
