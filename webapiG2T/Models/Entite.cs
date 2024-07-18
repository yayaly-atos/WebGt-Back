using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Entite
    {
        public int Id { get; set; }
        public string NomEntite { get; set; }
        public Boolean ResponsableEntite { get; set; }
        public ICollection<Utilisateur>? Utilisateurs { get; set; }
        public ICollection<Incident>? Incidents { get; set; }
        public ICollection<Agent> Agents { get; set; }
    }
}
