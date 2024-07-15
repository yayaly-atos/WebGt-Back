namespace webapiG2T.Models.Dto
{
    public class ContactDto
    {

        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Telephone { get; set; }
        public string Adresse { get; set; }
        public string StatutContact { get; set; }
        public int CompteId { get; set; }

    }
}
