namespace G2T.Models
{
    public class Profil
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public ICollection<Utilisateur> Utilisateurs { get; set; }
    }
}
