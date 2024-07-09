using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Utilisateur : IdentityUser
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string telephone { get; set; }

        [EnumDataType(typeof(Profil))]
        public Profil Profil { get; set; }
        public EntiteEnCharge EntiteEnCharge { get; set; }
    }
}
