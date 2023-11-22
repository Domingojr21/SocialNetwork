using RedSocial.Core.Application.Dtos.Account;

namespace RedSocial.Core.Application.Interfaces.Services
{
    public interface IAccountServices
    {
        Task<AuthenticationResponse> Authenticate(AuthenticationRequest request);
        Task<string> ConfirmAccount(string UserId, string token);
        Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, string origin);
        Task<RegisterResponse> RegisterBasicUser(RegisterRequest request, string origin);
        Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request);
        Task SingOut();
        Task<RegisterRequest> GetUserById(string UserId);

        Task<RegisterRequest> GetIdByUser(string User);

        Task<bool> ValidateUsers(string UserId);




    }
}