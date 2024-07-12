using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Forms;

namespace webapiG2T.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginModel model);
        Task<RegisterResponse> Register(RegisterModel model);
        Task Logout();
    }
}
