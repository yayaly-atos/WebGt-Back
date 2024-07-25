namespace webapiG2T.Models.Dto
{
    public class CreateIncidentDtocs
    {
        public int Id { get; set; }
        public int CanalId { get; set; }
        public int MotifId { get; set; }
        public int SousMotifId { get; set; }
        public string Description { get; set; }
        public string? Commentaire { get; set; }
        public string StatutIncident { get; set; }
        public int ContactId { get; set; }
        public int ServiceId { get; set; }
        public int PrioriteId { get; set; } 
        public int TeleconseillerId { get; set; }
    }
}
