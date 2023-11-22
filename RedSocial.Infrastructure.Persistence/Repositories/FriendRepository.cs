

using Microsoft.EntityFrameworkCore;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Domain.Entities;
using RedSocial.Infrastructure.Persistence.Contexts;
using RedSocial.Infrastructure.Persistence.Repositories.Generics;

namespace RedSocial.Infrastructure.Persistence.Repositories
{
    public class FriendRepository : GenericRepository<Friends>, IFriendRepository
    {
        private readonly ApplicationContext _dbContext;

        public FriendRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Friends>> GetAllFriends(string id)
        {
            var friends = await _dbContext.Friends.Where(a => a.UserId == id).ToListAsync();
            return friends;

        }
  
    }
    
 }

