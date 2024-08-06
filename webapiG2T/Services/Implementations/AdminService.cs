using Microsoft.AspNetCore.Identity;
using webapiG2T.Models.Forms;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class AdminService : IAdminService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AdminService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<Response> CreateRole(RoleModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Role))
            {
                return new Response { Status = "error", Message = "Le nom du rôle ne peut pas être vide." };
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (roleExists)
            {
                return new Response { Status = "error", Message = "Le rôle existe déjà." };
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(model.Role));
            if (result.Succeeded)
            {
                return new Response { Status = "success", Message = "Le rôle créé avec succès." };
            }

            return new Response { Status = "error", Message = "Échec de la création du rôle." };
        }
    }
}
