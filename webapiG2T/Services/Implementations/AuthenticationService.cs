using Azure;
using Azure.Core;
using G2T.Data;
using G2T.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text;
using webapiG2T.Models;
using webapiG2T.Models.Dto;
using webapiG2T.Models.Forms;
using webapiG2T.Services.Interfaces;
using Response = webapiG2T.Models.Forms.Response;

namespace webapiG2T.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly SignInManager<Utilisateur> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IRevoquerTokenService _tokenService;

        public AuthenticationService(UserManager<Utilisateur> userManager, SignInManager<Utilisateur> signInManager, RoleManager<IdentityRole> roleManager, DataContext _context, IConfiguration configuration, IRevoquerTokenService tokenService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
            this._context = _context;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<AuthenticationResponse> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                      new Claim("userId", user.Id),
                    new Claim(ClaimTypes.GivenName,user.Prenom),
                   new Claim(ClaimTypes.Surname,user.Nom),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                if (user.EntiteSupportId.HasValue)
                {
                    authClaims.Add(new Claim("Entite", user.EntiteSupportId.ToString()));
                }

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                Tuple<string, string, DateTime> response = GenerateToken(_configuration["JWT:Secret"], authClaims);

                await _tokenService.AddToken(response.Item1, response.Item2, response.Item3);

                return new AuthenticationResponse
                {
                    Id = response.Item1,
                    Token = response.Item2,
                    Expiration = response.Item3,

                    EntiteId=user.EntiteSupportId,

                    UserId = user.Id
                };
            }
            return null;
        }

        public async Task<Response> RegisterPretataire(RegisterModelTeleconseiller model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new Response { Status = "Error", Message = "L'utilisateur existe déjà!" };

         


            Utilisateur user = new Utilisateur()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Nom = model.Nom,
                Prenom = model.Prenom,
                PhoneNumber = model.Telephone,
                Adresse = model.Adresse,
                 PrestataireId= model.PrestaireId
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new Response { Status = "Error", Message = "L'échec de la création de l’utilisateur ! Veuillez vérifier les détails de l’utilisateur et réessayer." };

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            return new Response { Status = "Success", Message = "L'utilisateur créé avec succès!" };
        }

        public async Task<Response> Register(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new Response { Status = "Error", Message = "L'utilisateur existe déjà!" };

            if (model.EntiteId.HasValue && string.Equals(model.Role, "superviseur", StringComparison.OrdinalIgnoreCase))
            {
                var existingResponsable = await _context.Utilisateurs
                 .Join(_context.UserRoles, u => u.Id, ur => ur.UserId, (u, ur) => new { u, ur })
                 .Join(_context.Roles, ur => ur.ur.RoleId, r => r.Id, (ur, r) => new { ur.u, r })
                 .Where(x => x.u.EntiteSupportId == model.EntiteId.Value && x.r.Name == "superviseur")
                 .Select(x => x.u)
                 .FirstOrDefaultAsync();
                if (existingResponsable != null)
                {
                    return new Response { Status = "Error", Message = "L'entité est déjà assigné à un responsable." };
                }
            }


            Utilisateur user = new Utilisateur()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Nom = model.Nom,
                Prenom = model.Prenom,
                PhoneNumber = model.Telephone,
                Adresse = model.Adresse,
                EntiteSupportId = model.EntiteId
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new Response { Status = "Error", Message = "L'échec de la création de l’utilisateur ! Veuillez vérifier les détails de l’utilisateur et réessayer." };

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            return new Response { Status = "Success", Message = "L'utilisateur créé avec succès!" };
        }

        public Tuple<string, string, DateTime> GenerateToken(string secret, List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _configuration["JWT:ValidIssuer"],
                Audience = _configuration["JWT:ValidAudience"],
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jti = token.Id;
            var expiration = token.ValidTo;
            var tokenString = tokenHandler.WriteToken(token);

            return Tuple.Create(jti, tokenString, expiration);
        }

        public async Task Logout()
        {
            await signInManager.SignOutAsync();
        }

        public string DecodeTokenAndGetUserId(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]);

            try
            {
                
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                SecurityToken validatedToken;
                var principal = tokenHandler.ValidateToken(token, validationParameters, out validatedToken);

              
                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == "userId");
                return userIdClaim?.Value;
            }
            catch (Exception ex)
            {
               
                return null;
            }
        }



    }




}