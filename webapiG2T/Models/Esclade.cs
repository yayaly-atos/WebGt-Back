using G2T.Models;

namespace webapiG2T.Models
{
    public class Esclade
    {
        public int Id { get; set; }
        public EntiteSupport EntiteSupport { get; set; }

        public Incident Incident { get; set; }

        public string Commentaire { get; set; }

       
    }
}
