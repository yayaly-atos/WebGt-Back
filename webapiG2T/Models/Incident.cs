using G2T.Models.enums;
using System.ComponentModel.DataAnnotations;
using webapiG2T.Models;

namespace G2T.Models
{
    public class Incident
    {
        public int Id { get; set; }
        public Canal Canal { get; set; }
        public Motif Motif { get; set; }
    
        public string Description { get; set; }
        public string StatutIncident { get; set; }
        public DateTime? DateAffectation { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateEscalade {  get; set; }
        public DateTime? DateEcheance { get; set; }
        public DateTime? DateRelance { get; set; }
        public DateTime? DateResolution{ get; set; }
        public Boolean Escalade { get; set; }
        public Utilisateur? Agent { get; set; }
        public Utilisateur? Superviseur { get; set; }
        public Utilisateur Teleconseiller { get; set; }
        public Contact Contact { get; set; }
        public Service Service { get; set; }
        public Sla NiveauDurgence { get; set; }
        public EntiteSupport EntiteSupport { get; set; }
        public string? CommentaireEscalade { get; set; }
        public string? CommentaireAgent { get; set; }
        public string? CommentaireCloture { get; set; }
        public string? CommentaireTeleconseiller { get; set; }
    }
}
