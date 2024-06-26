﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class PostalOfficeConfiguration : IEntityTypeConfiguration<PostalOffice>
    {
        public void Configure(EntityTypeBuilder<PostalOffice> builder)
        {
            builder.ToTable("PostalOffices");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.OfficeName)
                .IsRequired()
                .HasMaxLength(500);
            builder.HasIndex(x => x.Location);
            builder.HasIndex(x => x.Address);
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();

            // Specify the navigation property instead of using HasOne<Area>()
            builder.HasOne(p => p.Area)
                   .WithMany(a => a.PostalOffices)
                   .HasForeignKey(p => p.AreaId);
        }
    }
}
