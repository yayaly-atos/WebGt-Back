using G2T.Models;

namespace webapiG2T.Models
{
    public class HistoriqueIncident
    {
        public int Id { get; set; }
        public int IncidentId { get; set; }
        public  string Nature { get; set; }
        public  string ValeurPrecedente { get; set; }
        public  string ValeurNouveau { get; set; }
        public  DateTime DateHistorique { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public ICollection<Incident> Incidents { get; set; }

    }
}
