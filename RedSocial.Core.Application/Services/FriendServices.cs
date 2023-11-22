using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services.Generics;
using RedSocial.Core.Application.ViewModels.Friends;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Services
{
    public class FriendServices : GenericServices<SaveFriendsViewModel, FriendsViewModel, Friends>, IFriendServices
    {
        private readonly IFriendRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly AuthenticationResponse _response;
        private readonly IAccountServices _accountServices;
        private readonly IPublicationServices _publicationServices;

        public FriendServices(IFriendRepository repository, IMapper mapper, IHttpContextAccessor http, IAccountServices accountServices, IPublicationServices publicationServices) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _http = http;
            _accountServices = accountServices;
            _response = _http.HttpContext.Session.Get<AuthenticationResponse>("user");
            _publicationServices = publicationServices;
        }


        public async Task<List<FriendsViewModel>> GetAllWithInclude()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "Comments" });
            
            List<FriendsViewModel> result = new List<FriendsViewModel>();

            foreach (var item in list)
            {
                FriendsViewModel vm = new FriendsViewModel
                {
                    Id = item.Id,
                    UserId = item.UserId,
                    FriendId = item.FriendId,

                };

                 var a = await _accountServices.GetUserById(item.FriendId);

                vm.UserName = a.UserName;
                vm.FriendLastName = a.LastName;
                vm.FriendName = a.FirstName;
                vm.FriendPhoto = a.Photo;
                vm.FriendPublications = await _publicationServices.GetPublicationsFriends(item.FriendId);
                result.Add(vm);
            }

            var filtro = result.Where(a => a.UserId == _response.Id).ToList();

            return filtro;
           
        }

        

        public async Task Add1(SaveFriendsViewModel vm)
        { 
            var a = await _accountServices.GetIdByUser(vm.FriendId);

            
            var add = new Friends {

                FriendId = a.Id,
                UserId = vm.UserId
            
            };

           await _repository.Add(add);


        }




      

      



    }
}
