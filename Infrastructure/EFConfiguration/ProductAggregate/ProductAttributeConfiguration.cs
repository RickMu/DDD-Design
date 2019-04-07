using Domain.ProductAttributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class ProductAttributeConfiguration: IEntityTypeConfiguration<ProductAttribute>
    {
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.Property(e => e.Name).HasColumnName("AttributeName").HasColumnName("varchar(10)").IsRequired();

            builder.HasKey(ProductConfiguration.DBInfo.PK_Name, "AttributeName");
            builder.HasMany(e => e.AttributeOptions)
                .WithOne()
                .HasForeignKey(ProductConfiguration.DBInfo.PK_Name, "AttributeName")
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}