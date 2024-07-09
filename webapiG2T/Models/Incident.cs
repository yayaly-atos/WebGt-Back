using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public Canal Canal { get; set; }
        public Motif Motif { get; set; }
        public SousMotif SousMotif { get; set; }
        public string Description { get; set; }
        public string Commentaire { get; set; }

        [EnumDataType(typeof(StatutIncident))]
        public StatutIncident Statut { get; set; }
        public EntiteEnCharge EntiteEnCharge { get; set; }
    }
}
