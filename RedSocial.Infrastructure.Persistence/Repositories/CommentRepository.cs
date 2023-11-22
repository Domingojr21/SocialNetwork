

using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infrastructure.Persistence.Contexts;
using RedSocial.Infrastructure.Persistence.Repositories.Generics;

namespace RedSocial.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comments>, ICommentRepository
    {
        private readonly ApplicationContext _dbContext;

        public CommentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Comments> GetAllComments(int PublicationsId)
        {
            return _dbContext.Comments.Where(a => a.PublicationsId == PublicationsId).ToList();

        }
    }
}
