using G2T.Models;

namespace webapiG2T.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllServicesAsync();
        Task<Service> GetServiceByIdAsync(int id);
        Task CreateServiceAsync(Service service);
        Task UpdateServiceAsync(Service service);
      
    }
}
