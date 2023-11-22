using Microsoft.AspNetCore.Mvc;
using MiniProyectoRedSocial.Middlewares;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Comments;

namespace MiniProyectoRedSocial.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentServices _commentServices;
        private readonly ValidateUserSession _validateUserSession;
        private readonly IPublicationServices _publicationServices;
        public CommentsController(ICommentServices commentServices, ValidateUserSession validateUserSession, IPublicationServices publicationServices)
        {
            _commentServices = commentServices;
            _validateUserSession = validateUserSession;
            _publicationServices = publicationServices;

        }

        [HttpPost]
        public async Task<IActionResult> Create(string content, int publicationsid, string userid)
        {
            if (!_validateUserSession.HasUser())
            {
                return RedirectToRoute(new { controller = "Users", action = "Index" });
            }

            if (!ModelState.IsValid)
            {
                ViewBag.Publications = await _publicationServices.GetAll();
                ViewBag.Comments = await _commentServices.GetAll();

                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }


            SaveCommentsViewModel comments = new SaveCommentsViewModel();
            comments.PublicationsId = publicationsid;
            comments.UserId = userid;
            comments.Content = content;

            await _commentServices.Add(comments);




            return RedirectToRoute(new { controller = "Home", action = "Index" });

        }
    }
}
