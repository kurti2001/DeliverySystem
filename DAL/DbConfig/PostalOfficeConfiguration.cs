﻿using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.DbConfig
{
    internal class PostalOfficeConfiguration : IEntityTypeConfiguration<PostalOffice>
    {
        public void Configure(EntityTypeBuilder<PostalOffice> builder) 
        {
            builder.ToTable("PostalOffice");
            builder.HasKey(x => x.PostalOfficeId);
            builder.Property(x => x.OfficeName)
                .IsRequired()
                .HasMaxLength(12);
            builder.HasIndex(x => x.Location);
            builder.Property(x => x.Address)
                .HasMaxLength(500)
                .IsRequired();
            builder.Property(x => x.PhoneNumber)
                .HasMaxLength(20)
                .IsRequired();
        }
    }
}