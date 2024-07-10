using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Facture
    {

        public int CompteId { get; set; }
        public int ServiceId { get; set; }
        public Compte? Compte { get; set; }
        public Service? Service { get; set; }
        public decimal Montant { get; set; }
        public DateTime DateFacturation { get; set; }
        public DateTime DatePaiement { get; set; }
        public DateTime DateExpiration { get; set; }
        public string StatutFacture { get; set; }
    }
}
