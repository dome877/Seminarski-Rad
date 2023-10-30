namespace Seminarski_Rad.Models
{
    public class CartItem
    {
        public Proizvod Proizvod { get; set; }
        public int Količina { get; set; }

        public decimal GetTotal()
        {
            return Proizvod.Cijena * Količina;
        }
    }
}
