using Microsoft.EntityFrameworkCore;

public class ExcelDbContext : DbContext
{
    public DbSet<TableEntity> Tables { get; set; }
    public DbSet<ClassEntity> Classes { get; set; }
    public DbSet<AccountEntity> Accounts { get; set; }
    public DbSet<OpeningBalanceEntity> OpeningBalances { get; set; }
    public DbSet<ClosingBalanceEntity> ClosingBalances { get; set; }
    public DbSet<TurnoverEntity> Turnovers { get; set; }
}