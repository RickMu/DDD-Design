using Domain.Customer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EFConfiguration
{
    public class SellSignupConfiguration: IEntityTypeConfiguration<SellSignup>
    {
        public void Configure(EntityTypeBuilder<SellSignup> builder)
        {
            builder.Property(e => e.SignupEmail).HasColumnName("Email")
                .HasColumnType("varchar(50)").IsRequired();

            builder.HasKey(ProductSellConfiguration.DBInfo.PK_Name);
        }
    }
}