using System.ComponentModel.DataAnnotations.Schema;

namespace Seminarski_Rad.Models
{
    public class NarudzbaItem
    {
        public int Id { get; set; }
        public int NarudzbaId { get; set; }
        public int ProizvodId { get; set; }
        public int Količina { get; set; }
        [Column(TypeName = "decimal(9,2)")]
        public decimal Ukupno { get; set; }
        
        [NotMapped]
        public string? ProizvodNaziv { get; set; }
    }
}
