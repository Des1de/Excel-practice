using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

public class TableEntity
{ 
    [Key]
    public Guid TableId { get; set; }
    public string BankName { get; set; } = null!;
    public string TableName { get; set; } = null!; 
    public string Period { get; set; } = null!; 
    public DateTime Date { get; set; }
    public string Currency { get; set; } = null!; 
    public IEnumerable<ClassEntity> Classes = null!; 

}