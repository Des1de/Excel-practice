using System.Reflection;
using Microsoft.EntityFrameworkCore;

public class ExcelDbContext : DbContext
{
    public ExcelDbContext(DbContextOptions<ExcelDbContext> options) : 
        base(options)
    {
            
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public DbSet<TableEntity> Tables { get; set; }
    public DbSet<ClassEntity> Classes { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<OpeningBalanceEntity> OpeningBalances { get; set; }
    public DbSet<ClosingBalanceEntity> ClosingBalances { get; set; }
    public DbSet<TurnoverEntity> Turnovers { get; set; }
}