using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MiniProyectoRedSocial.Middlewares;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.ViewModels.Users;

namespace MiniProyectoRedSocial.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserServices _services;

        public UsersController(IUserServices services)
        {
            _services = services;
        }


        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _services.Login(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }


        public async Task<IActionResult> LogOut()
        {
            await _services.SignOut();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUsersViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUsersViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];

            vm.Photo = UploadFile(vm.File, vm.Email);

            RegisterResponse response = await _services.Register(vm, origin);

            
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _services.ConfirmEmail(userId, token);
            return View("ConfirmEmail", response);
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ForgotPassword()
        {
            return View(new ForgotPasswordViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            ForgotPasswordResponse response = await _services.ForgotPassword(vm, origin);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(string token)
        {
            return View(new ResetPasswordViewModel { Token = token });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            ResetPasswordResponse response = await _services.ResetPassword(vm);
            if (response.HasError)
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
            return RedirectToRoute(new { controller = "Users", action = "Index" });
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        private string UploadFile(IFormFile file, string Email, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }



            //obtener ruta directorio
            string basepath = $"/Images/Users/{Email}";
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
