namespace webapiG2T.Models.Dto
{
    public class UtilisateurDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? Adresse { get; set; }
        public Boolean? Disponiblite { get; set; }
        public string? EntiteSupportId { get; set; }
        public Boolean? Actif { get; set; }
    }
}
