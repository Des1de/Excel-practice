using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TurnoverConfiguration : IEntityTypeConfiguration<TurnoverEntity>
{
    public void Configure(EntityTypeBuilder<TurnoverEntity> builder)
    {
        builder.ToTable("Turnovers");

        builder.HasKey(t => t.TurnoverId); 

        builder.Property(t => t.Credit)
            .IsRequired(); 

        builder.Property(t => t.Debit)
            .IsRequired();
    }
}