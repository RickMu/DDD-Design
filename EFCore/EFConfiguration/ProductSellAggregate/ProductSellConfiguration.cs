using Domain.ProductSells;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class ProductSellConfiguration: IEntityTypeConfiguration<ProductSell>
    {
        public class DBInfo
        {
            public const string PK_Name = "ProductSellId";
        }
        public void Configure(EntityTypeBuilder<ProductSell> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            builder.HasKey(e => e.Identity);
            builder.Property(e => e.Identity).HasColumnName(DBInfo.PK_Name).ValueGeneratedNever();

            builder.Property(e => e.IsReleased).IsRequired();
            builder.Property(e => e.IsReleasable).IsRequired();

            builder.HasMany(e => e.Signups)
                .WithOne()
                .HasForeignKey(DBInfo.PK_Name)
                .IsRequired();

            builder.HasMany(e => e.Combinations)
                .WithOne()
                .HasForeignKey(DBInfo.PK_Name)
                .IsRequired();
            
        }
    }
}