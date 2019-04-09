using Domain.ProductAttributes.Factory;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class AttributeOptionConfiguration: IEntityTypeConfiguration<AttributeOption>
    {
        public void Configure(EntityTypeBuilder<AttributeOption> builder)
        {
            builder.Property(e => e.Value).HasColumnName("Value").IsRequired().ValueGeneratedNever();

            builder.HasKey(ProductConfiguration.DBInfo.PK_Name,
                ProductAttributeConfiguration.DBInfo.PARTIAL_KEY_NAME,
                "Value");
        }
    }
}