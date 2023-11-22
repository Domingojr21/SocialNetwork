using AutoMapper;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Core.Application.ViewModels.Users;

namespace RedSocial.Core.Application.Services
{
    public class UserServices : IUserServices
    {
        private readonly IAccountServices _services;
        private readonly IMapper _mapper;

        public UserServices(IAccountServices services, IMapper mapper)
        {
            _services = services;
            _mapper = mapper;
        }

        public async Task<string> ConfirmEmail(string userId, string token)
        {
            return await _services.ConfirmAccount(userId, token);
        }

      
        public async Task<AuthenticationResponse> Login(LoginViewModel vm)
        {

            AuthenticationRequest loginRequest = _mapper.Map<AuthenticationRequest>(vm);
            AuthenticationResponse userResponse = await _services.Authenticate(loginRequest);
            return userResponse; ;
        }

        public async Task<RegisterResponse> Register(SaveUsersViewModel vm, string origin)
        {
            RegisterRequest registerRequest = _mapper.Map<RegisterRequest>(vm);
            return await _services.RegisterBasicUser(registerRequest, origin);
        }

        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel vm, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(vm);
            return await _services.ForgotPassword(forgotRequest, origin);
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel vm)
        {
            ResetPasswordRequest resetRequest = _mapper.Map<ResetPasswordRequest>(vm);
            return await _services.ResetPassword(resetRequest);
        }

        public async Task SignOut()
        {
            await _services.SingOut();
        }

        
    }
}
