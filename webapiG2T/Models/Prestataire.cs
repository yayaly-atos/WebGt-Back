using G2T.Models;

namespace webapiG2T.Models
{
    public class Prestataire 

    {
        public int Id { get; set; }
        public Utilisateur utilisateur { get; set; }

        public string NomPrestateur { get; set; }
        public Boolean Responsable { get; set; }
    }
}
