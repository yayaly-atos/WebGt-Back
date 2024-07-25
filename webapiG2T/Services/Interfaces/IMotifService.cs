using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface IMotifService
    {
        Task<string> GetMotifNomByIdAsync(int id);
        Task<List<Motif>> GetAllMotifsAsync();
    }
}
