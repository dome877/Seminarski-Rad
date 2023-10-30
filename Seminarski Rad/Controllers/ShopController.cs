using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Extensions;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Controllers
{
    // stavljen posebni controller zbog promjene u Layoutu iz Container fluid u Container
    public class ShopController : Controller
    {
        private readonly string _sesijaKeyName = "_cart";
        private ApplicationDbContext _context;

        public ShopController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index(int? kategorijaID)
        {

            List<Proizvod> proizvodi = _context.Proizvod.ToList();
            

            if (kategorijaID != null)
            {
                proizvodi = (from proizvod in _context.Proizvod
                            join proizvodKat in _context.KategorijaProizvoda on proizvod.Id equals proizvodKat.ProizvodId
                            where proizvodKat.KategorijaId == kategorijaID
                            select new Proizvod
                            {
                                Id = proizvod.Id,
                                Naziv = proizvod.Naziv,
                                Opis = proizvod.Opis,
                                Količina = proizvod.Količina,
                                Cijena = proizvod.Cijena

                            }).ToList();
            }

            ViewBag.Categories = _context.Kategorija.Select(c => new SelectListItem()
            {
                Value = c.Id.ToString(),
                Text = c.Naziv

            }).ToList();

            return View(proizvodi);
        }
        public IActionResult Order(List<string> errors)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sesijaKeyName);
            if (cart == null) cart = new List<CartItem>();

            if (cart.Count == 0)
            {
                TempData["Message"] = "Morate napunite košaricu kako bi napravili narudžbu!";
                return RedirectToAction(nameof(Index));
            }

            decimal sum = 0;
            ViewBag.TotalPrice = cart.Sum(item => sum = item.GetTotal());

            ViewBag.Errors = errors;

            return View(cart);
        }
        [HttpPost]
        public IActionResult CreateOrder(Narudzba narudzba)
        {
            // 1. Izvucite sve iz košarice
            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sesijaKeyName);
            if (cart == null) cart = new List<CartItem>();
            // 2. Provjerite ima li nešto u košarici, ako nema preusmjerite na proizvode
            if (cart.Count == 0)
            {
                TempData["Message"] = "Morate napunite košaricu kako bi napravili narudžbu!";
                return RedirectToAction(nameof(Index));
            }

            // 3. Ako ima spremite u narudzbu i stavke narudzbe

            // placeholder za greške
            List<string> modelErrors = new List<string>();
            // je li ispravan model
            if (ModelState.IsValid)
            {
                // spremam narudžbu da dobijem primarni ključ
                var user = _context.Users.SingleOrDefault(u => u.UserName == User.Identity.Name);
                // provjeriti je li korisnik prijavljen i ako je povezati ga s narudžbom
                if (user != null) narudzba.UserId = user.Id;
                // sistemski dodajemo datum na narudžbu
                // ne želimo dati korisniku
                narudzba.DatumKreiran = DateTime.Now;

                _context.Narudzba.Add(narudzba);
                _context.SaveChanges();

                // izvlačim primarni ključ
                int orderId = narudzba.Id;

                // spremam listu cartItema u orderIteme
                foreach (var item in cart)
                {
                    NarudzbaItem orderItem = new NarudzbaItem()
                    {
                        NarudzbaId = orderId,
                        ProizvodId = item.Proizvod.Id,
                        Količina = item.Količina,
                        Ukupno = item.GetTotal()
                    };

                    // spremam orderIteme
                    _context.NarudzbaItem.Add(orderItem);
                    _context.SaveChanges();
                }

                // brišem sesiju
                HttpContext.Session.SetObjectAsJson(_sesijaKeyName, string.Empty);

                TempData["Success"] = "Zahvaljujemo vam na vašoj narudžbi!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var modelError in modelState.Errors)
                    {
                        modelErrors.Add(modelError.ErrorMessage);
                    }
                }
            }

            return RedirectToAction(nameof(Order), new { errors = modelErrors });
        }
    }
}

