using G2T.Models;

namespace webapiG2T.Models
{
    public class Priorite
    {
        public int Id { get; set; }

        public string Nom{ get; set; }

        public DateTime Latence { get; set; }

        public ICollection<Incident>  Incidents { get; set; }
    }
}
