using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class GeneratePackageConfiguration : IEntityTypeConfiguration<GeneratePackage>
    {
        public void Configure(EntityTypeBuilder<GeneratePackage> builder)
        {
            builder.ToTable("GeneratedPackages");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                   .IsRequired();
            builder.Property(x=> x.SenderInformation)
                   .IsRequired();
            builder.Property(x=> x.Email)
                   .IsRequired();
            builder.Property(x=>x.CreatedDate)
                   .IsRequired();
            builder.Property(x=> x.DestinationAddress)
                   .IsRequired(); 
            builder.Property(x=> x.DestinationZipCode)
                   .IsRequired();
            builder.Property(x => x.SentAddress)
                   .IsRequired();
           builder.Property(x=> x.SentZipCode)
                   .IsRequired();
            builder.Property(x => x.Weight)
                   .IsRequired();
            
            
        }
    }
}
