using Microsoft.AspNetCore.Mvc;
using MiniProyectoRedSocial.Middlewares;
using RedSocial.Core.Application.Interfaces.Services;


namespace MiniProyectoRedSocial.Controllers
{
    public class PublicationsController : Controller
    {

        public readonly IPublicationServices _services;
        public readonly ICommentServices _servicesComments;
        public readonly ValidateUserSession _validateUserSession;
        public PublicationsController(IPublicationServices services, ICommentServices servicesComments, ValidateUserSession validateUserSession)
        {
            _services = services;
            _servicesComments = servicesComments;
            _validateUserSession = validateUserSession;
        }

     
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            await _services.Delete(id);

            string basePath = $"/Images/Publications/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (Directory.Exists(path))
            {
                DirectoryInfo directory = new(path);

                foreach (FileInfo file in directory.GetFiles())
                {
                    file.Delete();
                }
                foreach (DirectoryInfo folder in directory.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }


            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }


        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }



            //obtener ruta directorio
            string basepath = $"/Images/Publications/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basepath}");

            //crear carpeta si no existe
            if (!Directory.Exists(path))
            {

                Directory.CreateDirectory(path);

            }

            //obtener ruta del archivo
            Guid guid = Guid.NewGuid();
            FileInfo fileinfo = new(file.FileName);
            string filename = fileinfo.Name + fileinfo.Extension;

            string finalpath = Path.Combine(path, filename);

            using (var stream = new FileStream(finalpath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return Path.Combine(basepath, filename);
        }
    }
}
