public class AccountEntity
{
    public Guid AccountId { get; set; }
    public Guid ClassId { get; set; }
    public int AccountNumber { get; set; }
    public OpeningBalanceEntity OpeningBalance { get; set; } = null!; 
    public TurnoverEntity Revolution { get; set; } = null!; 
    public ClosingBalanceEntity ClosingBalance { get; set; } = null!; 
}