
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

public class ExcelService : IExcelSerivce
{
    private readonly ExcelDbContext _context; 

    public ExcelService(ExcelDbContext context)
    {
        _context = context; 
    }

    public async Task ConvertToDatabase(string filePath)
    {
        using(var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet(1); 

            var range = worksheet.RangeUsed(); 

            if(range == null)
            {
                throw new Exception("Table convertion error");
            }

            var tableEntity = new TableEntity()
            {
                BankName = range.Cell("A1").Value.ToString(),
                TableName = range.Cell("A2").Value.ToString(), 
                Period = range.Cell("A3").Value.ToString(),
                Date = DateTime.Parse(range.Cell("A6").Value.ToString()),
                Currency = range.Cell("G6").Value.ToString(),
                Classes = new List<ClassEntity>()
            };
            
            ClassEntity? currentClass = null;

            int classCount = 1; 

            for(int i = 9; i<=range.RowCount(); i++)
            {
                if(range.Cell($"A{i}").Value.ToString().StartsWith("КЛАСС"))
                {
                    if(currentClass is not null) tableEntity.Classes.Add(currentClass);
                    currentClass = new ClassEntity()
                    {
                        ClassName = range.Cell($"A{i}").Value.ToString(),
                        ClassNumber = classCount++,
                        Accounts = new List<AccountEntity>()
                    };
                    
                }

                else if(range.Cell($"A{i}").Value.ToString().StartsWith("ПО КЛАССУ") ||
                    range.Cell($"A{i}").Value.ToString().StartsWith("БАЛАНС") )
                    continue;

                else if(int.Parse(range.Cell($"A{i}").Value.ToString())/100!=0)
                {
                    var account = new AccountEntity()
                    {
                        AccountNumber = int.Parse(range.Cell($"A{i}").Value.ToString()),
                        OpeningBalance = new OpeningBalanceEntity()
                        {
                            Active = decimal.Parse(range.Cell($"B{i}").Value.ToString()),
                            Passive = decimal.Parse(range.Cell($"C{i}").Value.ToString())
                        },
                        Turnover = new TurnoverEntity()
                        {
                            Debit = decimal.Parse(range.Cell($"D{i}").Value.ToString()),
                            Credit = decimal.Parse(range.Cell($"E{i}").Value.ToString())
                        },
                        ClosingBalance = new ClosingBalanceEntity()
                        {
                            Active = decimal.Parse(range.Cell($"F{i}").Value.ToString()),
                            Passive = decimal.Parse(range.Cell($"G{i}").Value.ToString())
                        }
                    };

                    

                    if(currentClass is not null)
                    {
                        currentClass.Accounts.Add(account);
                    }
                    else 
                    {
                        throw new Exception("Table convertion error");
                    }
                }
            }

            _context.Tables.Add(tableEntity);
            foreach(var classEntity in tableEntity.Classes)
            {
                _context.Classes.Add(classEntity);
                foreach(var accountEntity in classEntity.Accounts)
                {
                    _context.Accounts.Add(accountEntity); 
                    _context.OpeningBalances.Add(accountEntity.OpeningBalance); 
                    _context.Turnovers.Add(accountEntity.Turnover); 
                    _context.ClosingBalances.Add(accountEntity.ClosingBalance);
                }
            }
            await _context.SaveChangesAsync();
        }
    }

    public async Task<TableEntity> GetTable(Guid tableId)
    {
        return await _context.Tables.Where(t => t.TableId == tableId).AsNoTracking()
            .Include(t => t.Classes).ThenInclude(c => c.Accounts).ThenInclude(a => a.Turnover)
            .Include(t => t.Classes).ThenInclude(c => c.Accounts).ThenInclude(a => a.ClosingBalance)
            .Include(t => t.Classes).ThenInclude(c => c.Accounts).ThenInclude(a => a.OpeningBalance).FirstAsync();
    }

    public async Task<IEnumerable<TableEntity>> GetTableEntities()
    {
        return await _context.Tables.AsNoTracking().ToListAsync();
    }
}