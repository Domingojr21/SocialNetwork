using RedSocial.Core.Application.ViewModels.Users;
using RedSocial.Core.Application.Helpers;

namespace MiniProyectoRedSocial.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasUser()
        {
            ForgotPasswordViewModel userViewModel = _httpContextAccessor.HttpContext.Session.Get<ForgotPasswordViewModel>("user");

            if (userViewModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
