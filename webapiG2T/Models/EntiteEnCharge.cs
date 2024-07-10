using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class EntiteEnCharge
    {
        public int Id { get; set; }
        public string NomEntiteEnCharge { get; set; }
        public Boolean Responsable { get; set; }
        public ICollection<Utilisateur>? Utilisateurs { get; set; }
        public ICollection<Incident>? Incidents { get; set; }
    }
}
