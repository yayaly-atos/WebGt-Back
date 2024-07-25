using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface ISousMotifService
    {
        Task<string> GetSousMotifNomByIdAsync(int id);
        Task<List<SousMotif>> GetAllSousMotifsAsync();
    }
}
