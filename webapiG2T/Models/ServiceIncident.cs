using G2T.Models;

namespace webapiG2T.Models
{
    public class ServiceIncident
    {
        public int ServiceId { get; set; }
        public int IncidentId { get; set; }
        public Service Service { get; set; }
        public Incident Incident { get; set; }
        public DateTime DateAffectation { get; set; }
    }
}
