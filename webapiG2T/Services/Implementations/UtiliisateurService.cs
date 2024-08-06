using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models.Dto;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class UtiliisateurService : IUtIlisateurService
    {

        private readonly DataContext _context;

        public UtiliisateurService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<UtilisateurDto>> GetUsersAgentByEntite(int entiteID)
        {
            var usersInRole = await (from user in _context.Users
                                     join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                     join role in _context.Roles on userRole.RoleId equals role.Id
                                     where role.Name == "Agent" && ((Utilisateur)user).EntiteSupportId == entiteID
                                     select new UtilisateurDto
                                     {
                                         Id = user.Id,
                                         UserName = user.UserName,
                                         Email = user.Email,
                                         Nom = user.Nom,
                                         Prenom = user.Prenom,
                                         Adresse = user.Adresse,
                                         Disponiblite = user.Disponiblite,
                                         Actif = user.Actif
                                     }).ToListAsync();

            return usersInRole;
        }

        public async Task<List<UtilisateurDto>> GetUsersTeleconseiller()
        {
            var usersInRole = await (from user in _context.Users
                                     join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                     join role in _context.Roles on userRole.RoleId equals role.Id
                                     where role.Name == "Teleconseiller"
                                     select new UtilisateurDto
                                     {
                                         Id = user.Id,
                                         UserName = user.UserName,
                                         Email = user.Email,
                                         Nom = user.Nom,
                                         Prenom = user.Prenom,
                                         Adresse = user.Adresse,
                                         Disponiblite = user.Disponiblite,
                                         Actif = user.Actif
                                     }).ToListAsync();

            return usersInRole;
        }

        public async Task<List<UtilisateurDto>> GetUsersSuperviseur()
        {
            var usersInRole = await (from user in _context.Users
                                     join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                     join role in _context.Roles on userRole.RoleId equals role.Id
                                     where role.Name == "Superviseur"
                                     select new UtilisateurDto
                                     {
                                         Id = user.Id,
                                         UserName = user.UserName,
                                         Email = user.Email,
                                         Nom = user.Nom,
                                         Prenom = user.Prenom,
                                         Adresse = user.Adresse,
                                         Disponiblite = user.Disponiblite,
                                         Actif = user.Actif
                                     }).ToListAsync();

            return usersInRole;
        }
        public async Task<List<UtilisateurDto>> GetAgents()
        {
            var usersInRole = await (from user in _context.Users
                                     join userRole in _context.UserRoles on user.Id equals userRole.UserId
                                     join role in _context.Roles on userRole.RoleId equals role.Id
                                     where role.Name == "Agent"
                                     select new UtilisateurDto
                                     {
                                         Id = user.Id,
                                         UserName = user.UserName,
                                         Email = user.Email,
                                         Nom = user.Nom,
                                         Prenom = user.Prenom,
                                         Adresse = user.Adresse,
                                         Disponiblite = user.Disponiblite,
                                         Actif = user.Actif
                                     }).ToListAsync();

            return usersInRole;

        }



    }
}
