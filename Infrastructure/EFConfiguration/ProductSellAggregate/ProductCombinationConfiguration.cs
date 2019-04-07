using Domain.ProductSells;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class ProductCombinationConfiguration: IEntityTypeConfiguration<ProductCombination>
    {
        public class DBInfo
        {
            public const string PARTIAL_KEY_NAME = "ProductCombinationId";
        }
        public void Configure(EntityTypeBuilder<ProductCombination> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.Property(e => e.Identity).HasColumnName("ProductCombinationId").ValueGeneratedNever();
            //Composite Key
            builder.HasKey(ProductSellConfiguration.DBInfo.PK_Name, DBInfo.PARTIAL_KEY_NAME);
            
            builder.Property(e => e.SignupCount).HasColumnName("SignupCount");

            builder.HasMany(e => e.Combination)
                .WithOne()
                .HasForeignKey(
                    ProductSellConfiguration.DBInfo.PK_Name,
                    DBInfo.PARTIAL_KEY_NAME
                )
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.OwnsOne(e => e.ProductPrice);
        }
    }
}