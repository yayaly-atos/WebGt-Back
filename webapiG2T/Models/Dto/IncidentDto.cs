namespace webapiG2T.Models.Dto
{

    public class IncidentDto
    {
        public int Id { get; set; }
        public string NomCanal { get; set; }
        public string NomMotif { get; set; }
        public string Description { get; set; }
        public string StatutIncident { get; set; }
        public DateTime? DateAffectation { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime? DateEscalade { get; set; }
        public DateTime? DateEcheance { get; set; }
        public DateTime? DateRelance { get; set; }
        public DateTime? DateResolution { get; set; }
        public bool Escalade { get; set; }
        public string? AgentId { get; set; }
        public string SuperviseurId { get; set; }
        public string TeleconseillerId { get; set; }
        public int ContactId { get; set; }
        public  string  NomService { get; set; }
        public int NiveauDurgenceId { get; set; }
        public int EntiteSupportId { get; set; }
        public string? CommentaireEscalade { get; set; }
        public string? CommentaireAgent { get; set; }
        public string? CommentaireCloture { get; set; }
        public string? CommentaireTeleconseiller { get; set; }
    }
}
