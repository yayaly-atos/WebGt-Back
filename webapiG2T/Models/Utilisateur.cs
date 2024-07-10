using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Utilisateur : IdentityUser
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public EntiteEnCharge EntiteEnCharge { get; set; }
    }
}
