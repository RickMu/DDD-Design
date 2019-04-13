using Domain.ProductAttributes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class ProductAttributeConfiguration: IEntityTypeConfiguration<ProductAttribute>
    {
        public class DBInfo
        {
           public const string PARTIAL_KEY_NAME = "Name";
        }
        public void Configure(EntityTypeBuilder<ProductAttribute> builder)
        {
            builder.Property(e => e.Name).HasColumnName(DBInfo.PARTIAL_KEY_NAME).IsRequired();
            builder.Property(e => e.AttributeType).HasColumnName("AttributeType").IsRequired();

            builder.HasKey(ProductConfiguration.DBInfo.PK_Name, DBInfo.PARTIAL_KEY_NAME);
            builder.HasMany(e => e.AttributeOptions)
                .WithOne()
                .HasForeignKey(ProductConfiguration.DBInfo.PK_Name, DBInfo.PARTIAL_KEY_NAME)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}