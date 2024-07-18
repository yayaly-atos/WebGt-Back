namespace G2T.Models
{
    public class Agent
    {
        public int Id { get; set; }
        public Utilisateur Utilisateur { get; set; }
        public Entite Entite { get; set; }
    }
}