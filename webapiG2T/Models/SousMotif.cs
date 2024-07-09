namespace G2T.Models
{
    public class SousMotif
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Incident> Incidents { get; set; }
    }
}
