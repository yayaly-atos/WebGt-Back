using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface IEntiteSupport
    {
        Task<List<EntiteSupport>> GetAllEntitesAsync();
    }
}
