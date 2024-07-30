using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface ICanalService
    {
        Task<string> GetCanalNomByIdAsync(int id);
        Task<List<Canal>> GetAllCanauxAsync();
        public Task<Canal> CreateCanalAsync(Canal newCanal);
        public Task<bool> UpdateCanalAsync(Canal updatedCanal);
    }
}
