using webapiG2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface IprestataireService
    {
        Task<List<Prestataire>> GetAllPrestatairesAsync();
        Task<Prestataire> GetPrestataireByIdAsync(int id);
        Task<Prestataire> CreatePrestataireAsync(Prestataire newPrestataire);
        Task UpdatePrestataireAsync(Prestataire updatedPrestataire);
        Task DeletePrestataireAsync(int id);
    }
}
