using G2T.Models;
using webapiG2T.Models.Dto;

namespace webapiG2T.Services.Interfaces
{
    public interface ISousMotifService
    {
        Task<string> GetSousMotifNomByIdAsync(int id);
        Task<List<SousMotif>> GetAllSousMotifsAsync();
      

        public Task<bool> UpdateSousMotifAsync(SousMotifDto updatedSousMotif);

        public Task<SousMotif> CreateSousMotifAsync(SousMotifDto newSousMotif);
    }
}
