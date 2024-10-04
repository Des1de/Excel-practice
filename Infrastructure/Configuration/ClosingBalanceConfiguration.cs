using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClosingBalanceConfiguration : IEntityTypeConfiguration<ClosingBalanceEntity>
{
    public void Configure(EntityTypeBuilder<ClosingBalanceEntity> builder)
    {
        builder.ToTable("ClosingBalances");

        builder.HasKey(b => b.ClosingBalanceId); 

        builder.Property(b => b.Active)
            .IsRequired(); 

        builder.Property(b => b.Passive)
            .IsRequired();
    }
}