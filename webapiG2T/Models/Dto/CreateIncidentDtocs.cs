namespace webapiG2T.Models.Dto
{
    public class CreateIncidentDtocs
    {
        public int Id { get; set; }
        public int CanalId { get; set; }
        public int SousMotifId { get; set; }
        public string Description { get; set; }
  
     
     
        public int ContactId { get; set; }
        public int ServiceId { get; set; }
        public int NiveauDurgenceId { get; set; }
  
    
        public string? CommentaireTeleconseiller { get; set; }
    }
}
