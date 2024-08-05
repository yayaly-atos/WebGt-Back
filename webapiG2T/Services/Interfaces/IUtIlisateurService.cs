using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IUtIlisateurService
    {

        Task<List<UtilisateurDto>> GetUsersAgentByEntite(int entiteID);

      
        Task<List<UtilisateurDto>> GetUsersTeleconseiller();

        Task<List<UtilisateurDto>> GetUsersSuperviseur();

        Task<List<UtilisateurDto>> GetAgents();

    }
}
