using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seminarski_Rad.Models
{
    public class Kategorija
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Naziv { get; set; }

        [ForeignKey("KategorijaId")]
        public List<KategorijaProizvoda>? KategorijaProizvoda { get; set; }
    }
}
