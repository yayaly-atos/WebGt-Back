using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface IMotifService
    {
        Task<string> GetMotifNomByIdAsync(int id);
        Task<List<Motif>> GetAllMotifsAsync();
        public Task<Motif> CreateMotifAsync(Motif newMotif);
        Task<bool> UpdateMotifAsync(Motif updatedMotif);
    }

}