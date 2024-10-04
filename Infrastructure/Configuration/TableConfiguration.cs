using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class TableConfiguration : IEntityTypeConfiguration<TableEntity>
{
    public void Configure(EntityTypeBuilder<TableEntity> builder)
    {
        builder.ToTable("Tables");

        builder.HasKey(t => t.TableId);

        builder.Property(t => t.BankName)
            .IsRequired();
        
        builder.Property(t => t.TableName)
            .IsRequired(); 

        builder.Property(t => t.Period)
            .IsRequired(); 
        
        builder.Property(t => t.Date)
            .IsRequired(); 

        builder.Property(t => t.Currency)
            .IsRequired(); 

        builder.HasMany(t => t.Classes)
            .WithOne()
            .HasForeignKey(c => c.TableId);
    }
}