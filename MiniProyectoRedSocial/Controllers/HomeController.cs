using Microsoft.AspNetCore.Mvc;
using MiniProyectoRedSocial.Middlewares;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Publications;



namespace MiniProyectoRedSocial.Controllers
{
    public class HomeController : Controller
    {
       
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPublicationServices _publicationServices;
        private readonly ICommentServices _commentServices;
        private readonly IFriendServices _friendServices;
        public HomeController(ValidateUserSession validateUserSession, IPublicationServices publicationServices, ICommentServices commentServices, IFriendServices friendServices)
        {           
            _validateUserSession = validateUserSession;
            _publicationServices = publicationServices;
            _commentServices = commentServices;
            _friendServices = friendServices;
        }
        
        public async Task<IActionResult> Index()
        {   
            if(! _validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            ViewBag.Publications = await _publicationServices.GetAllWithInclude();      

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePublicationsViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Publications = await _publicationServices.GetAll();
                return View("Index",vm);
            }
            vm.Date = DateTime.Now;

            SavePublicationsViewModel publications = await _publicationServices.Add(vm);

            if (vm.File != null)
            {
                if (publications != null && publications.Id != 0)
                {
                    publications.Photo = UploadFile(vm.File, publications.Id);
                    await _publicationServices.Update(publications, publications.Id);
                }
            }
            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
        public async Task<IActionResult> Edit(int id, string userid)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }


            SavePublicationsViewModel publications = await _publicationServices.GetById(id);
            publications.UserId = userid;
            //publications.Comments = await _servicesComments.GetAllWithInclude();

            return View("Edit",publications);

        }


        [HttpPost]
        public async Task<IActionResult> EditPost(SavePublicationsViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Publications = await _publicationServices.GetAll();
                return View("Index",vm);
            }

            vm.Date = DateTime.Now;

           
            SavePublicationsViewModel publications = await _publicationServices.GetById(vm.Id);
 
                vm.Photo = UploadFile(vm.File, publications.Id, true, publications.Photo);
            
            await _publicationServices.Update(vm, vm.Id);

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