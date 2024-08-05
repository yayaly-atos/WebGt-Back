using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;
using webapiG2T.Models;

namespace G2T.Models
{
    public class EntiteSupport
    {
        public int Id { get; set; }
        public string NomEntite { get; set; }
        public ICollection<Incident> Incidents { get; set; }

        public ICollection<Utilisateur> Superviseurs { get; set; }

    }
}
