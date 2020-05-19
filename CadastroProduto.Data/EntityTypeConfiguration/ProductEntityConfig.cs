using CadastroProduto.Library.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CadastroProduto.Data.EntityTypeConfiguration
{
    public class ProductEntityConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(x => x.ProductId);

            builder.Property(x => x.ProductId)
                    .HasColumnName("ProductId")
                    .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                    .HasColumnName("Name")
                    .HasMaxLength(200)
                    .IsUnicode(false)
                    .IsRequired();

            builder.Property(x => x.Price)
                    .HasColumnName("Price")
                    .IsRequired();

            builder.Property(x => x.UrlImage)
                    .HasColumnName("UrlImage")
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .IsRequired();

            builder.Property(x => x.Status)
                    .HasColumnName("Status")
                    .HasColumnName("Status")
                    .IsRequired();

            builder.Property(x => x.Created)
                    .HasColumnName("Created")
                    .IsRequired();

            builder.Property(x => x.Updated)
                   .HasColumnName("Updated")
                   .IsRequired();
        }
    }
}
