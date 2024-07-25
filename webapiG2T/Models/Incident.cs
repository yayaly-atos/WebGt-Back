using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;
using webapiG2T.Models;

namespace G2T.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public Canal Canal { get; set; }
        public Motif Motif { get; set; }
        public SousMotif SousMotif { get; set; }
        public string Description { get; set; }
        public string? Commentaire { get; set; }
        public string StatutIncident { get; set; }
        public Contact Contact { get; set; }
        public Service Service { get; set; }
        public  Boolean Disponiblite { get; set; }
        public DateTime? DateEcheance { get; set; }
        public Priorite Priorite { get; set; }

        public ICollection<AgentIncident>? AgentIncidents { get; set; }
        public Teleconseiller Teleconseiller { get; set; }

        public ICollection<Esclade> Esclades { get; set; }
    }
}
