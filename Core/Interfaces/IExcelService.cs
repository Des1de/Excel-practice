public interface IExcelSerivce
{
    Task ConvertToDatabase(string filePath); 
    Task<TableEntity> GetTable(Guid tableId); 
    Task<IEnumerable<TableEntity>> GetTableEntities(); 
}