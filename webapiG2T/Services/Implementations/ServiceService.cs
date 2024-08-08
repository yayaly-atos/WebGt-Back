using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class ServiceService: IServiceService
    {

        private readonly DataContext _context;

        public ServiceService(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service> GetServiceByIdAsync(int id)
        {
            return await _context.Services.FindAsync(id);
        }

        public async Task CreateServiceAsync(Service service)
        {
            _context.Services.Add(service);
            await _context.SaveChangesAsync();
        }

        public async Task<Service> UpdateServiceAsync(Service serviceUpdate)
        {
            var service = await _context.Services.FindAsync(serviceUpdate.Id);
            if (service == null)
            {
                return null;
            }

            _context.Entry(service).CurrentValues.SetValues(serviceUpdate);
           
            await _context.SaveChangesAsync();
            return service;
        }

       
    }
}
