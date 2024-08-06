using G2T.Data;
using Microsoft.EntityFrameworkCore;
using webapiG2T.Models;
using webapiG2T.Models.Forms;
using webapiG2T.Services.Interfaces;

namespace webapiG2T.Services.Implementations
{
    public class HistoriqueIncidentService : IHistoriqueIncident
    {
        private readonly DataContext _context;

        public HistoriqueIncidentService(DataContext context)
        {
            _context = context;
        }


        async Task<Response> IHistoriqueIncident.AjouterHistorique(HistoriqueIncident historiqueIncident)
        {

            _context.Historiques.Add(historiqueIncident);
            await _context.SaveChangesAsync();
            return new Response
            {
                Status = "Succès",
                Message = "L'historique a été ajouté."
            };
        }
    }
}
