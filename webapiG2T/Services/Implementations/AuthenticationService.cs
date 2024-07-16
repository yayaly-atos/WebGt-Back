using Azure;
using G2T.Data;
using G2T.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using webapiG2T.Models.Forms;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly UserManager<Utilisateur> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(UserManager<Utilisateur> userManager, RoleManager<IdentityRole> roleManager, DataContext _context, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this._context = _context;
            _configuration = configuration;
        }
        
        public async Task<AuthenticationResponse> Login(LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                Tuple<string, DateTime>  response = GenerateToken(_configuration["JWT:Secret"], authClaims);

                return new AuthenticationResponse
                {
                    token = response.Item1,
                    expiration = response.Item2
                };
            }
            return null;
        }

        public async Task<RegisterResponse> Register(RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return new RegisterResponse { Status = "Error", Message = "User already exists!" };


            var entiteEnCharge = new Entite();
            if(model.Role == "Teleconseiller" || model.Role == "Admin")
            {
                entiteEnCharge = null;
            }
            else
            {
                entiteEnCharge = this.getEntite(model.EntiteSupport, model.isResponsable);
            }

            Utilisateur user = new Utilisateur()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                Nom = model.Nom,
                Prenom = model.Prenom,
                Entite = entiteEnCharge
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return new RegisterResponse { Status = "Error", Message = "User creation failed! Please check user details and try again." };

            if (!await roleManager.RoleExistsAsync(model.Role))
                await roleManager.CreateAsync(new IdentityRole(model.Role));

            if (await roleManager.RoleExistsAsync(model.Role))
            {
                await userManager.AddToRoleAsync(user, model.Role);
            }

            return new RegisterResponse { Status = "Success", Message = "User created successfully!" };
        }

        public Task Logout()
        {
            throw new NotImplementedException();
        }

        public static Tuple<string, DateTime>  GenerateToken(string secret, List<Claim> claims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            var expiration = token.ValidTo;
            var tokenString = tokenHandler.WriteToken(token);
            return Tuple.Create(tokenString, expiration);
        }

        public Entite getEntite(string nom, Boolean responsable)
        {
            var entite = _context.Entite.FirstOrDefault(e => e.NomEntite == nom && e.ResponsableEntite.Equals(responsable));
            return entite;
        }
    }
}
