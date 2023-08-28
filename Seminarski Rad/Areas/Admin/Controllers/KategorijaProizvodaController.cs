using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KategorijaProizvodaController : Controller
    {
        private ApplicationDbContext _context;

        public KategorijaProizvodaController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index(int proizvodID)
        {
            ViewBag.ProductId = proizvodID;
            var kategorijaproizvodaLista = _context.KategorijaProizvoda
                .Where(kp => kp.ProizvodId == proizvodID)
                .Select(kp => new KategorijaProizvoda()
                {
                    Id = kp.Id,
                    ProizvodId = kp.ProizvodId,
                    KategorijaId = kp.KategorijaId,
                    ProizvodNaziv = _context.Proizvod.SingleOrDefault(p => p.Id == kp.ProizvodId).Naziv,
                    KategorijaNaziv = _context.Kategorija.SingleOrDefault(x=>x.Id ==kp.KategorijaId).Naziv,
                });
            return View(kategorijaproizvodaLista);
            
        }

        public IActionResult Create(int proizvodID)
        {
            ViewBag.ProductId = proizvodID;


            ViewBag.Categories = _context.Kategorija
                .Select
                (
                    c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Naziv,
                    }
                ).ToList();

            if (TempData.ContainsKey("ErrorMessage"))
            {
                ModelState.AddModelError("", TempData["ErrorMessage"].ToString());
            }


            return View();
        }

        

        [HttpPost]
        public IActionResult Create(KategorijaProizvoda kategorijaproizvoda)
        {
            if (ModelState.IsValid)
            {
                var existingCategory = _context.KategorijaProizvoda
                    .FirstOrDefault(kp => kp.ProizvodId == kategorijaproizvoda.ProizvodId && kp.KategorijaId == kategorijaproizvoda.KategorijaId);

                if (existingCategory == null)
                {
                    _context.KategorijaProizvoda.Add(kategorijaproizvoda);
                    _context.SaveChanges();

                    return RedirectToAction(nameof(Index), new { proizvodID = kategorijaproizvoda.ProizvodId });
                }
                else
                {
                    TempData["ErrorMessage"] = "Kategorija već postoji za ovaj proizvod";
                    return RedirectToAction(nameof(Create), new { proizvodID = kategorijaproizvoda.ProizvodId });
                }
            }

            return View(kategorijaproizvoda);
        }

        public IActionResult Delete(int id)
        {
            var kategorijaproizvoda = _context.KategorijaProizvoda.FirstOrDefault(c => c.Id == id);

            if (kategorijaproizvoda == null)
            {
                return NotFound();
            }

            _context.KategorijaProizvoda.Remove(kategorijaproizvoda);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index), new { proizvodID = kategorijaproizvoda.ProizvodId });
        }


    }
}
