

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using RedSocial.Core.Application.Dtos.Account;
using RedSocial.Core.Application.Enums;
using RedSocial.Core.Application.Interfaces.Services;
using RedSocial.Infrastructure.Identity.Entities;
using System.Text;

namespace RedSocial.Infrastructure.Identity.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IEmailServices _emailServices;

        public AccountServices(UserManager<Users> userManager, SignInManager<Users> signInManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailServices = emailServices;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            AuthenticationResponse response = new AuthenticationResponse();
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No hay Cuentas Registrada con {request.Email} ";
                return response;
            }

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, false);
            if (!result.Succeeded)
            {

                response.HasError = true;
                response.Error = $"Credenciales Erroneas para {request.Email} ";
                return response;
            }

            if (!user.EmailConfirmed)
            {

                response.HasError = true;
                response.Error = $"La cuenta no se ha confirmado para {request.Email} ";
                return response;
            }

            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
            response.Photo = user.Photo;

            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;


            return response;

        }

        public async Task SingOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<RegisterResponse> RegisterBasicUser(RegisterRequest request, string origin)
        {
            RegisterResponse response = new RegisterResponse();

            response.HasError = false;

            var SameUser = await _userManager.FindByNameAsync(request.UserName);

            if (SameUser != null)
            {
                response.HasError = true;
                response.Error = $"El Usuario '{request.UserName}' Ya existe";
                return response;
            }

            var SameEmail = await _userManager.FindByEmailAsync(request.Email);

            if (SameEmail != null)
            {
                response.HasError = true;
                response.Error = $"El Correo '{request.Email}' Ya existe ";
                return response;
            }

            var user = new Users
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber,
                Photo = request.Photo

            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());
                var urlVerification = await EmailVerification(user, origin);
                await _emailServices.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
                {
                    To = user.Email,
                    Body = $"Por favor Verifica el correo a través de esta URL {urlVerification}",
                    Subject = "Confirmar Registro"


                });
            }
            else
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error Registrando el usuario ";
                return response;
            }

            return response;
        }

        public async Task<string> ConfirmAccount(string UserId, string token)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                return "No existe una Cuenta con ese Usuario";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var results = await _userManager.ConfirmEmailAsync(user, token);
            if (results.Succeeded)
            {
                return $"Cuenta Confirmada pra {user.Email}, ya puedes usar la aplicacion.";
            }
            else
            {
                return $"Un error ha occurrido confirmando el correo {user.Email}";
            }

        }
        public async Task<ForgotPasswordResponse> ForgotPassword(ForgotPasswordRequest request, string origin)
        {
            ForgotPasswordResponse response = new ForgotPasswordResponse();

            response.HasError = false;

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No existen cuentas Registradas con {request.Email}";
                return response;

            }

            var urlVerification = await SendForgotPassword(account, origin);

            await _emailServices.SendAsync(new Core.Application.Dtos.Email.EmailRequest()
            {
                To = account.Email,
                Body = $"Por restablece tu cuenta visitando esta URL{urlVerification}",
                Subject = "Confirmar Registro"


            });


            return response;
        }

        public async Task<ResetPasswordResponse> ResetPassword(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new ResetPasswordResponse();

            response.HasError = false;

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"No existen cuentas Registradas con {request.Email}";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));
            var results = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);
            if (!results.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error reseteando la contraseña {request.Email}";
                return response;
            }

            return response;

        }

        private async Task<string> EmailVerification(Users user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "Users/ConfirmEmail";
            var url = new Uri(string.Concat($"{origin}/", route));
            var urlVerification = QueryHelpers.AddQueryString(url.ToString(), "UserId", user.Id);
            urlVerification = QueryHelpers.AddQueryString(urlVerification, "token", code);


            return urlVerification;
        }

        private async Task<string> SendForgotPassword(Users user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "Users/ResetPassword";
            var url = new Uri(string.Concat($"{origin}/", route));
            var urlVerification = QueryHelpers.AddQueryString(url.ToString(), "token", code);

            return urlVerification;
        }

        public async Task<RegisterRequest> GetUserById(string UserId)
        {
            
                var user = await _userManager.FindByIdAsync(UserId);
           
            if (user == null)
                {
                    return null;
                }
                
                RegisterRequest DTO = new();
                DTO.Photo = user.Photo;
                DTO.UserName = user.UserName;
                DTO.LastName = user.LastName;
                DTO.FirstName = user.FirstName;
                
            
            return DTO;
            
        }

        public async Task<RegisterRequest> GetIdByUser(string User)
        {
            var user = await _userManager.FindByNameAsync(User);

            if (user == null)
            {
                return null;
            }

            RegisterRequest Dto = new();
            Dto.Id = user.Id;

            return Dto;
        }

        public async Task<bool> ValidateUsers(string User)
        {
            

            var user = await _userManager.FindByNameAsync(User);

            if(user == null)
            {
                return true;
            }
                    
            return false;

        }

        
    }
}
