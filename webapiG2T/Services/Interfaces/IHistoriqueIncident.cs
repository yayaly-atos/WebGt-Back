using webapiG2T.Models;
using webapiG2T.Models.Forms;

namespace webapiG2T.Services.Interfaces
{
    public interface IHistoriqueIncident
    {
        Task<Response> AjouterHistorique(HistoriqueIncident historiqueIncident);
    }
}
