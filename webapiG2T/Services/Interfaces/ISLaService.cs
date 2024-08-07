using G2T.Models;

using webapiG2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface ISLaService
    {
        Task<List<Sla>> GetAllSla();
        Task<Sla> CreateSlaAsync(Sla newSla);
        Task UpdateSlaAsync(Sla updatedSla);
        Task DeleteSlaAsync(int id);
        Task<Sla> GetSlaById(int id);
    }
}
