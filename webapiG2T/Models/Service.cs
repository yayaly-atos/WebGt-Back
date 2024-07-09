using System.ComponentModel.DataAnnotations;

namespace G2T.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string NomService { get; set; }

        [EnumDataType(typeof(TypeService))]
        public TypeService TypeService { get; set; }
        public ICollection<Facture> Factures { get; set; }
    }
}
