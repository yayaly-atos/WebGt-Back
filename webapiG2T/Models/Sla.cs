using G2T.Models;

namespace webapiG2T.Models
{
    public class Sla
    {
        public int Id { get; set; }

        public string Nom{ get; set; }

        public String Latence { get; set; }

        public ICollection<Incident>  Incidents { get; set; }
    }
}
