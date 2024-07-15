namespace webapiG2T.Models.Dto
{
    public class IncidentDto
    {
        public int Id { get; set; }
        public string Canal { get; set; }
        public string Motif { get; set; }
        public string SousMotif { get; set; }
        public string Description { get; set; }
        public string Commentaire { get; set; }
        public string StatutIncident { get; set; }
        public string EntiteEnCharge { get; set; }
        public ContactDto Contact { get; set; }
    }
}
