public class AccountEntity
{
    public Guid AccountId { get; set; }
    public Guid ClassId { get; set; }
    public int AccountNumber { get; set; }
    public Guid OpeningBalanceId { get; set; }
    public OpeningBalanceEntity OpeningBalance { get; set; } = null!; 
    public Guid TurnoverId { get; set; }
    public TurnoverEntity Turnover { get; set; } = null!; 
    public Guid ClosingBalanceId { get; set;}
    public ClosingBalanceEntity ClosingBalance { get; set; } = null!; 
}