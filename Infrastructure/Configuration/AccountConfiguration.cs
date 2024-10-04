using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AccountConfiguration : IEntityTypeConfiguration<AccountEntity>
{
    public void Configure(EntityTypeBuilder<AccountEntity> builder)
    {
        builder.ToTable("Accounts"); 

        builder.HasKey(a => a.AccountId); 

        builder.Property(a => a.AccountNumber)
            .IsRequired(); 

        builder.HasOne(a => a.OpeningBalance)
            .WithOne()
            .HasForeignKey<AccountEntity>(a => a.OpeningBalanceId)
            .IsRequired(); 
        
        builder.HasOne(a => a.ClosingBalance)
            .WithOne()
            .HasForeignKey<AccountEntity>(a => a.ClosingBalanceId)
            .IsRequired(); 
        
        builder.HasOne(a => a.Turnover)
            .WithOne()
            .HasForeignKey<AccountEntity>(a => a.TurnoverId)
            .IsRequired(); 

    }
}