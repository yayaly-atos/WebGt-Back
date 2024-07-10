using Microsoft.AspNetCore.Mvc;
using webapiG2T.Models.Forms;

namespace webapiG2T.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<IActionResult> Login(LoginModel model);
        Task Logout();
    }
}
