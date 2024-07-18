using G2T.Models;

namespace webapiG2T.Models
{
    public class Teleconseiller
    {
        public int Id { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public Prestataire Prestataire { get; set; }
        public Boolean Responsable { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}
