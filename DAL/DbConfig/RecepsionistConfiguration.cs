using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class RecepsionistConfiguration : IEntityTypeConfiguration<Recepsionist>
    {
        public void Configure(EntityTypeBuilder<Recepsionist> builder)
        {
            builder.ToTable("Recepsionists");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);
            builder.HasIndex(x => x.PhoneNumber)
                .IsUnique();
        }
        
    }
}
