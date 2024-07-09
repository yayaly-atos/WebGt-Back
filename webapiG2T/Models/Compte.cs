using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Compte
    {
        public int Id { get; set; }
        public string NomCompte { get; set; }

        [EnumDataType(typeof(TypeCompte))]
        public TypeCompte TypeCompte { get; set; }
        public DateTime DateOuverture { get; set; }

        [EnumDataType(typeof(Statut))]
        public Statut Statut { get; set; }
        public decimal Solde { get; set; }
        public ICollection<Contact> Contacts { get; set; }
        public ICollection<Facture> Factures { get; set; }
    }
}
