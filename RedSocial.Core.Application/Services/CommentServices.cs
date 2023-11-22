

using AutoMapper;
using Microsoft.AspNetCore.Http;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Helpers;
using RedSocial.Core.Application.Interfaces.Repositories;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.Services.Generics;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Publications;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Services
{
    public class CommentServices : GenericServices<SaveCommentsViewModel, CommentsViewModel, Comments>, ICommentServices
    {
        private readonly ICommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _http;
        private readonly AuthenticationResponse _response;
        private readonly IAccountServices _accountServices;


        public CommentServices(ICommentRepository repository, IMapper mapper, IHttpContextAccessor http, IAccountServices accountServices) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _http = http;
            _accountServices = accountServices;
            _response = _http.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public async Task<List<CommentsViewModel>> GetAllComments(int PublicationsId)
        {

            var list = _repository.GetAllComments(PublicationsId);
           

            List<CommentsViewModel> comments = new List<CommentsViewModel>();   


               foreach(var pancho in list)
            {
                var user = await _accountServices.GetUserById(pancho.UserId);
                CommentsViewModel vm = new CommentsViewModel();

                vm.Content = pancho.Content;
                vm.PublicationsId = pancho.PublicationsId;
                vm.UserId = pancho.UserId;
                vm.PhotoUrl = user.Photo;
                vm.UserName = user.UserName;
                
                comments.Add(vm);
            }
        
            return comments;


        }

        


    }

}
