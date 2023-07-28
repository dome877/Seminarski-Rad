using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Seminarski_Rad.Models
{
    public class Proizvod
    {
        public int Id { get; set; }
        [Required]
        [StringLength(200, MinimumLength = 2)]
        public string Naziv { get; set; }
        [Required]
        public string Opis { get; set; }
        [Required]
        public int Količina { get; set; }
        [Required]
        [Column(TypeName = "decimal(9,2)")]
        public decimal Cijena { get; set; }

        [ForeignKey("ProizvodId")]
        public List<KategorijaProizvoda>? KategorijaProizvoda { get; set; }

        [ForeignKey("ProizvodId")]
        public List<NarudzbaItem>? NarudzbaItem { get; set; }

    }
}
