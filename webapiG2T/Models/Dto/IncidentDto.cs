namespace webapiG2T.Models.Dto
{

    public class IncidentDto
    {
        public int Id { get; set; }
        public String CanalNom { get; set; }
        public String MotifNom { get; set; }
        public String SousMotifNom { get; set; }
        public string Description { get; set; }
        public string? Commentaire { get; set; }
        public string StatutIncident { get; set; }
        public int ContactId { get; set; }
        public int ServiceId { get; set; }
        public int TeleconseillerId { get; set; }
    }
}
