
using RedSocial.Core.Application.Interfaces.Services.Generics;
using RedSocial.Core.Application.ViewModels.Friends;
using RedSocial.Core.Application.ViewModels.Publications;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IFriendServices : IGenericServices<SaveFriendsViewModel, FriendsViewModel, Friends>
    {
        Task Add1(SaveFriendsViewModel vm);
        Task<List<FriendsViewModel>> GetAllWithInclude();
    
    }
}
