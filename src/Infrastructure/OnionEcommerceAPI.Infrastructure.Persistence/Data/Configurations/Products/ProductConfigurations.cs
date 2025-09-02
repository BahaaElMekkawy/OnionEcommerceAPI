﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnionEcommerceAPI.Core.Domain.Entities.Products;
using OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Base;

namespace OnionEcommerceAPI.Infrastructure.Persistence.Data.Configurations.Products
{
    public class ProductConfigurations : BaseAuditableEntityConfigurations<Product, int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder); //Configure the BaseEntity properties

            builder.Property(P => P.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(P => P.Description)
                .IsRequired();

            builder.Property(P => P.Price)
                .HasColumnType("decimal(9,2)");

            builder.HasOne(P => P.Brand)
                .WithMany()
                .HasForeignKey(P => P.BrandId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(P => P.Category)
              .WithMany()
              .HasForeignKey(P => P.CategoryId)
              .OnDelete(DeleteBehavior.SetNull);
        }
    }  
}
