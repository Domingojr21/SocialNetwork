

using AutoMapper;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.ViewModels.Comments;
using RedSocial.Core.Application.ViewModels.Friends;
using RedSocial.Core.Application.ViewModels.Publications;
using RedSocial.Core.Application.ViewModels.Users;
using RedSocial.Core.Domain.Entities;

namespace RedSocial.Core.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region UsersProfile
            
            CreateMap<AuthenticationRequest, LoginViewModel>()
                    .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
                    .ReverseMap();

            CreateMap<RegisterRequest, SaveUsersViewModel>()
                    .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
                    .ReverseMap();

            CreateMap<ForgotPasswordRequest, ForgotPasswordViewModel>()
                    .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
                    .ReverseMap();

            CreateMap<ResetPasswordRequest, ResetPasswordViewModel>()
                    .ForMember(x => x.HasError, opt => opt.Ignore())
                    .ForMember(x => x.Error, opt => opt.Ignore())
                    .ReverseMap();
            #endregion

            #region PublicationsProfile
            CreateMap<Publications, PublicationsViewModel>()  
            .ReverseMap()
            .ForMember(x => x.Created, opt => opt.Ignore())
            .ForMember(x => x.CreatedBy, opt => opt.Ignore())
            .ForMember(x => x.LastModified, opt => opt.Ignore())
            .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Publications, SavePublicationsViewModel>()
           .ReverseMap()
           .ForMember(x => x.Created, opt => opt.Ignore())
           .ForMember(x => x.CreatedBy, opt => opt.Ignore())
           .ForMember(x => x.LastModified, opt => opt.Ignore())
           .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
           .ForMember(x => x.Comments, opt => opt.Ignore());

            #endregion

            #region CommentsProfile
            CreateMap<Comments, CommentsViewModel>()
           .ReverseMap()
           .ForMember(x => x.Created, opt => opt.Ignore())
           .ForMember(x => x.CreatedBy, opt => opt.Ignore())
           .ForMember(x => x.LastModified, opt => opt.Ignore())
           .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());

            CreateMap<Comments, SaveCommentsViewModel>()
           .ReverseMap()
           .ForMember(x => x.Created, opt => opt.Ignore())
           .ForMember(x => x.CreatedBy, opt => opt.Ignore())
           .ForMember(x => x.LastModified, opt => opt.Ignore())
           .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
           .ForMember(x => x.Publications, opt => opt.Ignore());
            #endregion

            #region FriendsProfile
            CreateMap<Friends, FriendsViewModel>();
           
           
            #endregion
        }

    }
}
