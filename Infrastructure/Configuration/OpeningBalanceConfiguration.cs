using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class OpeningBalanceConfiguration : IEntityTypeConfiguration<OpeningBalanceEntity>
{
    public void Configure(EntityTypeBuilder<OpeningBalanceEntity> builder)
    {
        builder.ToTable("OpeningBalances");

        builder.HasKey(b => b.OpeningBalanceId); 

        builder.Property(b => b.Active)
            .IsRequired(); 

        builder.Property(b => b.Passive)
            .IsRequired();
    }
}