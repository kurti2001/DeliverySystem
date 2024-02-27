using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class AreaConfiguration : IEntityTypeConfiguration<Area>
    {
        public void Configure(EntityTypeBuilder<Area> builder)
        {
            builder.ToTable("Areas");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name)
                   .IsRequired()
                   .HasMaxLength(100);
            builder.Property(x => x.ZipCode)
                   .IsRequired()
                   .HasMaxLength(5);
            builder.Property(x => x.AreaInformation)
                   .IsRequired();
        }
    }
}