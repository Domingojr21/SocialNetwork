

using RedSocial.Core.Application.Interfaces.Services.Generics;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Publications;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public  interface ICommentServices : IGenericServices<SaveCommentsViewModel, CommentsViewModel, Comments>
    {
        

       Task<List<CommentsViewModel>> GetAllComments(int PublicationsId);

    }
}
