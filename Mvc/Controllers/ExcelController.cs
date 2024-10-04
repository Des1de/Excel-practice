using Microsoft.AspNetCore.Mvc;

public class ExcelController : Controller
{
    [HttpGet]
    public IActionResult UploadFile()
    {
        return View(); 
    }

    [HttpPost]
    public async Task<IActionResult> UploadFile(IFormFile excelFile)
    {
            if (excelFile == null || excelFile.Length == 0)
            {
                ModelState.AddModelError("", "Пожалуйста, выберите файл для загрузки.");
                return View("UploadFile");
            }

            // Проверка расширения файла
            var fileExtension = Path.GetExtension(excelFile.FileName);
            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                ModelState.AddModelError("", "Недопустимый формат файла. Пожалуйста, загрузите файл Excel (.xls или .xlsx).");
                return View("UploadFile");
            }

            Directory.CreateDirectory("UploadedFiles");

            // Путь для сохранения файла
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UploadedFiles", excelFile.FileName);

            // Сохранение файла
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await excelFile.CopyToAsync(stream);
            }

            // Успешное сообщение
            ViewBag.Message = "Файл успешно загружен!";
            return View("UploadFile");
        }
}