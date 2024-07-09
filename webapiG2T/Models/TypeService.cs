namespace G2T.Models
{
    public class TypeService
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Service> Services { get; set; }
    }
}
