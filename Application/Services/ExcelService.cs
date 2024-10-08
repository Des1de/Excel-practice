
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

public class ExcelService : IExcelSerivce
{
    private readonly ExcelDbContext _context; 

    public ExcelService(ExcelDbContext context)
    {
        _context = context; 
    }

    public async Task ConvertToDatabase(string filePath)
    {
        IWorkbook workbook;

        using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            // Определяем формат файла
            if (Path.GetExtension(filePath) == ".xls")
            {
                workbook = new HSSFWorkbook(fileStream); // Для .xls
            }
            else
            {
                workbook = new XSSFWorkbook(fileStream); // Для .xlsx
            }

            var worksheet = workbook.GetSheetAt(0); // Получаем первый лист

            var tableEntity = new TableEntity()
            {
                BankName = worksheet.GetRow(0).GetCell(0).ToString(),
                TableName = worksheet.GetRow(1).GetCell(0).ToString(),
                Period = worksheet.GetRow(2).GetCell(0).ToString(),
                Date = DateTime.Parse(worksheet.GetRow(5).GetCell(0).ToString()),
                Currency = worksheet.GetRow(5).GetCell(6).ToString(),
                Classes = new List<ClassEntity>()
            };

            ClassEntity? currentClass = null;
            int classCount = 1;

            // Итерируем по строкам, начиная с 9
            for (int i = 8; i <= worksheet.LastRowNum; i++)
            {
                var row = worksheet.GetRow(i);
                if (row == null) continue; // Пропускаем пустые строки

                string cellValueA = row.GetCell(0)?.ToString() ?? string.Empty;

                if (cellValueA.StartsWith("КЛАСС"))
                {
                    if (currentClass is not null) 
                        tableEntity.Classes.Add(currentClass);

                    currentClass = new ClassEntity()
                    {
                        ClassName = cellValueA,
                        ClassNumber = classCount++,
                        Accounts = new List<AccountEntity>()
                    };
                }
                else if (cellValueA.StartsWith("ПО КЛАССУ") || cellValueA.StartsWith("БАЛАНС"))
                {
                    continue; // Пропускаем строки с "ПО КЛАССУ" и "БАЛАНС"
                }
                else if (int.TryParse(cellValueA, out int accountNumber) && accountNumber / 100 != 0)
                {
                    var account = new AccountEntity()
                    {
                        AccountNumber = accountNumber,
                        OpeningBalance = new OpeningBalanceEntity()
                        {
                            Active = decimal.Parse(row.GetCell(1).ToString()),
                            Passive = decimal.Parse(row.GetCell(2).ToString())
                        },
                        Turnover = new TurnoverEntity()
                        {
                            Debit = decimal.Parse(row.GetCell(3).ToString()),
                            Credit = decimal.Parse(row.GetCell(4).ToString())
                        },
                        ClosingBalance = new ClosingBalanceEntity()
                        {
                            Active = decimal.Parse(row.GetCell(5).ToString()),
                            Passive = decimal.Parse(row.GetCell(6).ToString())
                        }
                    };

                    if (currentClass is not null)
                    {
                        currentClass.Accounts.Add(account);
                    }
                    else
                    {
                        throw new Exception("Table conversion error");
                    }
                }
            }

            if (currentClass is not null) 
                tableEntity.Classes.Add(currentClass); // Добавляем последний класс

            _context.Tables.Add(tableEntity);
            foreach (var classEntity in tableEntity.Classes)
            {
                _context.Classes.Add(classEntity);
                foreach (var accountEntity in classEntity.Accounts)
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