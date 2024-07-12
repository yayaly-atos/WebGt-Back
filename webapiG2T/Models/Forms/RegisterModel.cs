namespace webapiG2T.Models.Forms
{
    public class RegisterModel
    {
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string EntiteSupport { get; set; }
        public Boolean isResponsable { get; set; }
    }
}
