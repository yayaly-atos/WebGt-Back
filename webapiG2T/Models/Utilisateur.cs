using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using webapiG2T.Models;

namespace G2T.Models
{
    public class Utilisateur : IdentityUser
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? Adresse { get; set; }
        public Boolean? Disponiblite { get; set; }
        public Boolean? Actif {  get; set; } = true;

        public int? EntiteSupportId { get; set; }
        public EntiteSupport? EntiteSupportResponsable { get; set; }

        public int? PrestataireId { get; set; }
        public Prestataire? Prestataire { get; set; }






    }
}
