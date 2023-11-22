
using RedSocial.Core.Application.Interfaces.Repositories.Generics;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Interfaces.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comments>
    {
        List<Comments> GetAllComments(int PublicationsId);
    }
}
