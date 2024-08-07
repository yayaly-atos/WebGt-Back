using G2T.Models;
using Microsoft.AspNetCore.Identity;

namespace webapiG2T.Models
{
    public class Prestataire

    {
        public int Id { get; set; }

        public string NomPrestateur { get; set; }
        public ICollection<Utilisateur>? Utilisateurs { get; set; }
       
    }
}
