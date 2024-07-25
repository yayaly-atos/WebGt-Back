using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface ICanalService
    {
        Task<string> GetCanalNomByIdAsync(int id);
        Task<List<Canal>> GetAllCanauxAsync();
    }
}
