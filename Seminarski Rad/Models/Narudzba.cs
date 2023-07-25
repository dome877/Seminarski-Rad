using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Seminarski_Rad.Models
{
    public class Narudzba
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DatumKreiran { get; set; }
        [Required(ErrorMessage = "Ukupna cijena je obavezna!")]
        [Column(TypeName = "decimal(7,2)")]
        public decimal Ukupno { get; set; }
        public int? Popust { get; set; }
        [Required(ErrorMessage = "Ime je obavezno.")]
        [StringLength(50)]
        public string NarudzbaPrvoIme { get; set; }
        [Required(ErrorMessage = "Prezime je obavezno.")]
        [StringLength(50)]
        public string NarudzbaDrugoIme { get; set; }
        [Required(ErrorMessage = "Email adresa je potreba.")]
        [StringLength(100)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail nije ispravan.")]
        public string NarudzbaEmail { get; set; }
        [Required(ErrorMessage = "Telefonski broj je potreban.")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Telefonski broj nije ispravan.")]
        public string NarudzbaTelefon { get; set; }
        [Required(ErrorMessage = "Adresa je potrebna.")]
        [StringLength(150)]
        public string NarudzbaAdresa { get; set; }
        [Required(ErrorMessage = "Grad je potreban.")]
        [StringLength(50)]
        public string NarudzbaGrad { get; set; }
        [Required(ErrorMessage = "Zemlja je potrebna.")]
        [StringLength(50)]
        public string NarudzbaCountry { get; set; }
        [Required(ErrorMessage = "Poštanski broj je potreban.")]
        [StringLength(20)]
        public string NarudzbaPostanskibroj { get; set; }

        // DZ: Dodajte i Shipping atribute
        // svugdje gdje postoji Billing prefiks dodajte i Shipping
        // npr. ShippingFirstName...

        public string Poruka { get; set; }



        [ForeignKey("NarudzbaId")]
        public List<NarudzbaItem>? NarudzbaItem { get; set; }

    }
}
