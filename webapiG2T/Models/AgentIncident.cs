using G2T.Models;

namespace webapiG2T.Models
{
    public class AgentIncident
    {
        public int AgentId { get; set; }
        public int IncidentId { get; set; }
        public Agent Agent { get; set; }
        public Incident Incident { get; set; }
        public DateTime DateAffectation { get; set; }
        public string Commentaire { get; set; }
    }
}
