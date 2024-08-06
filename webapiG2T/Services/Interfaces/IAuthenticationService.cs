using G2T.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using webapiG2T.Models.Dto;
using webapiG2T.Models.Forms;

namespace webapiG2T.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<AuthenticationResponse> Login(LoginModel model);
        Task<Response> Register(RegisterModel model);

        Task<Response> RegisterPretataire(RegisterModelTeleconseiller model);
        Task Logout();

        string DecodeTokenAndGetUserId(string token);
        string DecodeTokenAndGetUEntiteId(string token);








    }
}
