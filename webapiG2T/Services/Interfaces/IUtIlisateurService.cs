using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IUtIlisateurService
    {

        Task<List<UtilisateurDto>> GetUsersAgentByEntite(String entiteID);

      
        Task<List<UtilisateurDto>> GetUsersTeleconseiller();

        Task<List<UtilisateurDto>> GetUsersSuperviseur();

        Task<List<UtilisateurDto>> GetAgents();

        Task<UtilisateurDto> GetAgentById(string idAgent);

        Task<UtilisateurDto> GetUserBYId(string idUser);

        Task<List<UtilisateurDto>> GetAdminById();


    }
}
