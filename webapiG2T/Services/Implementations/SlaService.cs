using G2T.Data;
using G2T.Models;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Controllers;
using webapiG2T.Models;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class SlaService : ISLaService
    {

        private readonly DataContext _context;

        public SlaService(DataContext context)
        {
            _context = context;
        }
        public async Task<List<Sla>> GetAllSla()
        {

            return await _context.Priorite.ToListAsync();

        }
    }
}
