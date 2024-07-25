using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class EntiteSupport
    {
        public int Id { get; set; }
        public string NomEntite { get; set; }
        public ICollection<Agent> Agents { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}
