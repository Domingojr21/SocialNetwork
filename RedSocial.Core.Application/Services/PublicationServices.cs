using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services.Generics;
using RedSocial.Core.Application.ViewModels.Publications;
using RedSocial.Core.Domain.Entities;


namespace RedSocial.Core.Application.Services
{
    public class PublicationServices : GenericServices<SavePublicationsViewModel, PublicationsViewModel, Publications>, IPublicationServices

    {
        private readonly IPublicationRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly AuthenticationResponse _response;
        private readonly IAccountServices _accountServices;
        private readonly ICommentServices _commentServices;

        public PublicationServices(IPublicationRepository repository, IMapper mapper, IHttpContextAccessor http, ICommentServices commentServices, IAccountServices accountServices) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _http = http;
            _commentServices = commentServices;
            _response = _http.HttpContext.Session.Get<AuthenticationResponse>("user");
            _accountServices = accountServices;
        }


        public async Task<List<PublicationsViewModel>> GetAllWithInclude()
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "Comments" });


            List<PublicationsViewModel> result = new List<PublicationsViewModel>();
       

            foreach(var item in list) {

                PublicationsViewModel vm = new PublicationsViewModel
                {
                    Photo = item.Photo,
                    Content = item.Content,
                    Date = item.Date,
                    Id = item.Id,
                    UserId = item.UserId,
                    Comments = await _commentServices.GetAllComments(item.Id)
                 };

                result.Add(vm);

            }

            return result;  


        }

        public async Task<List<PublicationsViewModel>> GetPublicationsFriends(string friendid)
        {
            var list = await _repository.GetAllWithInclude(new List<string> { "Comments" });

          

            List<PublicationsViewModel> result = new List<PublicationsViewModel>();


            foreach (var item in list)
            {
                
                PublicationsViewModel vm = new PublicationsViewModel
                {
                    Photo = item.Photo,
                    Content = item.Content,
                    Date = item.Date,
                    Id = item.Id,
                    UserId = item.UserId,
                    Comments = await _commentServices.GetAllComments(item.Id)
                };

                result.Add(vm);

            }

            var filtro = result.Where(a => a.UserId == friendid).ToList();

            return filtro;

        }

    

    }
}
