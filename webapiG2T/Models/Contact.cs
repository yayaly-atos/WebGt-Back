using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Contact
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string StatutContact { get; set; }
        public Compte Compte { get; set; }
        public ICollection<Incident>? Incidents { get; set; }
    }
}
