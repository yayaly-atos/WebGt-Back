namespace webapiG2T.Models.Dto
{

    public class IncidentDto
    {
        public int Id { get; set; }
        public string CanalNom { get; set; }
        public string MotifNom { get; set; }
        public string SousMotifNom { get; set; }
        public string Description { get; set; }
        public string? Commentaire { get; set; }
        public string StatutIncident { get; set; }
        public int ContactId { get; set; }
        public int ServiceId { get; set; }
        public int TeleconseillerId { get; set; }
        public bool Disponiblite { get; set; }  
        public DateTime? DateEcheance { get; set; } 
        public string PrioriteNom { get; set; }
    }
}
