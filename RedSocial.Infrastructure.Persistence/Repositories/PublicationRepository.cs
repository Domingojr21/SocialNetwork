
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infrastructure.Persistence.Contexts;
using RedSocial.Infrastructure.Persistence.Repositories.Generics;

namespace RedSocial.Infrastructure.Persistence.Repositories
{
    public class PublicationRepository : GenericRepository<Publications>, IPublicationRepository
    {
        private readonly ApplicationContext _dbContext;

        public PublicationRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


    }


}
