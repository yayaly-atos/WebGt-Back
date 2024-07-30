namespace G2T.Models
{
    public class Motif
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Incident>? Incidents { get; set; }
        public ICollection<SousMotif>? SousMotifs { get; set; }
    }
}
