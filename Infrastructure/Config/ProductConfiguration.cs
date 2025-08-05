using System;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Config;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        // Configuration logic for Product entity
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Name).IsRequired();
        // Additional configurations can be added here
    }
}

