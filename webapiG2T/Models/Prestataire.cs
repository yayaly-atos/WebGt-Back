using G2T.Models;

namespace webapiG2T.Models
{
    public class Prestataire 

    {
        public int Id { get; set; }
        public ICollection<Utilisateur> Utilisateurs { get; set; }
        public string NomPrestateur { get; set; }
    }
}
