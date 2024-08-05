using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface IUtIlisateurService
    {
        Task<List<UtilisateurDto>> GetUsersAgent();
    }
}
