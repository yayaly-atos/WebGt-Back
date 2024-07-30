namespace G2T.Models
{
    public class SousMotif
    {
        public int Id { get; set; }
        public string Nom { get; set; }

      
        public int MotifId { get; set; }  
        public Motif Motif { get; set; }  

        public ICollection<Incident>? Incidents { get; set; }


    }
}
