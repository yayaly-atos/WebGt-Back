using Azure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using webapiG2T.Models.Forms;
using webapiG2T.Services.Implementations;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Controllers
{
    [ApiController]
    [Route("webapig2t/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authService;
        private readonly IRevoquerTokenService _tokenService;

        public AuthenticationController(IAuthenticationService authService, IRevoquerTokenService tokenService)
        {
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _authService.Login(model);
            if (response != null)
            {
                return Ok(new
                {
                    token = response.Token,
                    expiration = response.Expiration
                });
            }
            return Unauthorized();
        }

       
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var response = await _authService.Register(model);
            if (response != null)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }
      
        [HttpPost]
        [Route("register-teleconseiller-prestataire")]
        public async Task<IActionResult> RegisterTelecnseiller([FromBody] RegisterModelTeleconseiller model)
        {
            var response = await _authService.RegisterPretataire(model);
            if (response != null)
            {
                return StatusCode(StatusCodes.Status200OK, response);
            }
            return StatusCode(StatusCodes.Status500InternalServerError, response);

        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                await _tokenService.RevoquerTokenAsync(token);
            }

            return StatusCode(StatusCodes.Status200OK, "Déconnecté(e) avec succès");
        }

        [HttpGet("getUserId")]
        public IActionResult GetUserId()
        {
           
            var token = Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token == null)
            {
                return Unauthorized("Token not provided.");
            }

           
            var userId = _authService.DecodeTokenAndGetUEntiteId(token);

            if (userId == null)
            {
                return Unauthorized("Invalid token.");
            }

            return Ok(new { UserId = userId });
        }





    }
}
