using G2T.Models;

using webapiG2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface ISLaService
    {
        Task<List<Sla>> GetAllSla();
    }
}
