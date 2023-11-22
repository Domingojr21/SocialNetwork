using RedSocial.Core.Application.Interfaces.Repositories.Generics;
using RedSocial.Core.Application.Interfaces.Services.Generics;
using RedSocial.Core.Application.ViewModels.Publications;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IPublicationServices : IGenericServices<SavePublicationsViewModel, PublicationsViewModel, Publications>
    {
        Task<List<PublicationsViewModel>> GetAllWithInclude();


        Task<List<PublicationsViewModel>> GetPublicationsFriends(string userid);

    }
}
