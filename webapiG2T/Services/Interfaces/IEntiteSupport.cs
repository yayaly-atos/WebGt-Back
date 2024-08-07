using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface IEntiteSupport
    {
        Task<List<EntiteSupport>> GetAllEntitesAsync();
        Task<EntiteSupport> GetEntiteByIdAsync(int id);
        Task<EntiteSupport> CreateEntiteAsync(EntiteSupport newEntite);
        Task UpdateEntiteAsync(EntiteSupport updatedEntite);
        Task DeleteEntiteAsync(int id);
    }
}
