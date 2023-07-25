using System.ComponentModel.DataAnnotations.Schema;

namespace Seminarski_Rad.Models
{
    public class KategorijaProizvoda
    {
        public int Id { get; set; }
        public int KategorijaId { get; set; }
        public int ProizvodId { get; set; }

        [NotMapped]
        public string? ProizvodNaziv { get; set; }
        [NotMapped]
        public string? KategorijaNaziv { get; set; }
    }
}
