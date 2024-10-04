public class ClassEntity
{
    public Guid ClassId { get; set; }
    public Guid TableId { get; set; }
    public string ClassName { get; set; } = null!; 
    public int ClassNumber { get; set; }
    public IEnumerable<AccountEntity> Accounts { get; set; } = null!; 

}