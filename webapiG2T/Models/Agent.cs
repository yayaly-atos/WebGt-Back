using webapiG2T.Models;

namespace G2T.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public EntiteSupport Entite { get; set; }
        public Boolean Responsable { get; set; }
        public ICollection<AgentIncident>? AgentIncidents { get; set; }
    }
}