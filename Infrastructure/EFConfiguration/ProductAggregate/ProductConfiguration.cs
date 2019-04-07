using System.Reflection.Metadata;
using Domain.Common.Domain;
using Domain.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class ProductConfiguration: IEntityTypeConfiguration<Product>
    {
        public class DBInfo
        {
            public const string PK_Name = "ProductId";
        }
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            //Ignore DomainEvents
            builder.Ignore(e => e.DomainEvents);
            //PrimaryKey never generated
            builder.HasKey(e => e.Identity);
            builder.Property(e => e.Identity).HasColumnName("ProductId").ValueGeneratedNever();

            builder.Property(e => e.BasePrice).HasColumnName("BasePrice").HasColumnType("decimal(10,4)");

            builder.HasMany(e => e.Attributes)
                .WithOne()
                .HasForeignKey(DBInfo.PK_Name)
                .IsRequired();
            
            builder.HasMany(e => e.ProductSells)
                .WithOne()
                .HasForeignKey(DBInfo.PK_Name)
                .IsRequired();
        }
    }
}