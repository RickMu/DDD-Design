using Domain.ProductSells;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class SelectedAttributeConfiguration: IEntityTypeConfiguration<SelectedAttribute>
    {
        public class DBInfo
        {
            public const string PARTIAL_KEY_NAME = "Name";
        }
        public void Configure(EntityTypeBuilder<SelectedAttribute> builder)
        {
            builder.HasKey(ProductSellConfiguration.DBInfo.PK_Name,
                ProductCombinationConfiguration.DBInfo.PARTIAL_KEY_NAME,
                DBInfo.PARTIAL_KEY_NAME);
            
            builder.Property(e => e.Name).HasColumnName("AttributeName").IsRequired();
            builder.Property(e => e.SelectedOption).HasColumnName("SelectedOption").IsRequired();
        }
    }
}