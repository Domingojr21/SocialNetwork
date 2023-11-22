using Microsoft.AspNetCore.Mvc;
using MiniProyectoRedSocial.Middlewares;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Friends;

namespace MiniProyectoRedSocial.Controllers
{
    public class FriendsController : Controller
    {
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPublicationServices _publicationServices;
        private readonly ICommentServices _commentServices;
        private readonly IFriendServices _friendServices;
        private readonly IAccountServices _accountServices;
        public FriendsController(ValidateUserSession validateUserSession, IPublicationServices publicationServices, ICommentServices commentServices, IFriendServices friendServices, IAccountServices accountServices)
        {
            _validateUserSession = validateUserSession;
            _publicationServices = publicationServices;
            _commentServices = commentServices;
            _friendServices = friendServices;
            _accountServices = accountServices;
        }
        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            ViewBag.Friends = await _friendServices.GetAllWithInclude();
            ViewBag.Publications = await _publicationServices.GetAllWithInclude();
            return View(new SaveFriendsViewModel());
        }
      
        
        [HttpPost]
        public async Task<IActionResult> Create(SaveFriendsViewModel vm)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Publications = await _publicationServices.GetAllWithInclude();
                ViewBag.Friends = await _friendServices.GetAllWithInclude();
                return View("Index", vm);
            }

            var error = await _accountServices.ValidateUsers(vm.FriendId);

            if (error == true)
            {
                vm.HasError = true;
                vm.Error = "El usuario ingresado No existe";
                ViewBag.Friends = await _friendServices.GetAllWithInclude();
                ViewBag.Publications = await _publicationServices.GetAllWithInclude();
                return View("Index", vm);
            }


            await _friendServices.Add1(vm);
            ViewBag.Publications = await _publicationServices.GetAllWithInclude();
            ViewBag.Friends = await _friendServices.GetAllWithInclude();

            return View("Index", vm);
        }


        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            await _friendServices.Delete(id);

            ViewBag.Friends = await _friendServices.GetAll();
            ViewBag.Publications = await _publicationServices.GetAllWithInclude();

            return View("Index", new SaveFriendsViewModel());

        }


        [HttpPost]
        public async Task<IActionResult> CreateComments(string content, int publicationsid, string userid)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }


            if (!ModelState.IsValid)
            {
                ViewBag.Publications = await _publicationServices.GetAllWithInclude();
                ViewBag.Friends = await _friendServices.GetAllWithInclude();

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }


            SaveCommentsViewModel comments = new SaveCommentsViewModel();
            comments.PublicationsId = publicationsid;
            comments.UserId = userid;
            comments.Content = content;

            await _commentServices.Add(comments);

            ViewBag.Publications = await _publicationServices.GetAllWithInclude();
            ViewBag.Friends = await _friendServices.GetAllWithInclude();

            return View("Index", new SaveFriendsViewModel());

        }


    }
}
