using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Controllers
{
    // stavljen posebni controller zbog promjene u Layoutu iz Container fluid u Container
    public class ShopController : Controller
    {
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
    }
}
