using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ClassConfiguration : IEntityTypeConfiguration<ClassEntity>
{
    public void Configure(EntityTypeBuilder<ClassEntity> builder)
    {
        builder.ToTable("Classes"); 

        builder.HasKey(c => c.ClassId);

        builder.Property(c => c.ClassName)
            .IsRequired(); 

        builder.Property(c => c.ClassNumber)
            .IsRequired();

        builder.HasMany(c => c.Accounts)
            .WithOne()
            .HasForeignKey(a=>a.ClassId);
    }
}