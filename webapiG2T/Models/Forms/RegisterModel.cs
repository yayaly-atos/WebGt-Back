namespace webapiG2T.Models.Forms
{
    public class RegisterModel
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string? Email { get; set; }
        public string? Telephone { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Adresse { get; set; }
        public string Role { get; set; }
        public int? EntiteId { get; set; }
        public Boolean? Disponible { get; set; }
        public Boolean? Actif { get; set; }
    }
}
