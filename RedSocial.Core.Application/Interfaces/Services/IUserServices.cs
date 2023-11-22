using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.ViewModels.Users;


namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IUserServices
    {
        Task<string> ConfirmEmail(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordViewModel vm, string origin);
        Task<AuthenticationResponse> Login(LoginViewModel vm);
        Task<RegisterResponse> Register(SaveUsersViewModel vm, string origin);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordViewModel vm);
        Task SignOut();
    }
}
