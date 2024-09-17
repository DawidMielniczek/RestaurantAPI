using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace RestaurantAPI.Controllers
{
    [Route("file")]
    [Authorize]
    public class FileController : Controller
    {
        public ActionResult GetFile([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory(); // zwraca scieżkę bazową do projektu
            
            var filePath = $"{rootPath}/PrivateFiles/{fileName}";

            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            // odczyt pliku i dodanie go do pamieci
           var fileContents = System.IO.File.ReadAllBytes(filePath);

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(filePath, out var contentType);

            return File(fileContents,contentType,fileName);
        }
        
    }
}
