using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IUtIlisateurService
    {
<<<<<<< HEAD
        Task<List<UtilisateurDto>> GetUsersAgent(int entiteID);
=======
        Task<List<UtilisateurDto>> GetUsersAgent();
        Task<List<UtilisateurDto>> GetUsersTeleconseiller();

        Task<List<UtilisateurDto>> GetUsersSuperviseur();
>>>>>>> 6353a43b9d1bafc7f04c4f47f6f1c018eb79848e
    }
}
