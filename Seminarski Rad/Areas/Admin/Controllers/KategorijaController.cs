using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class KategorijaController : Controller
    {
        private ApplicationDbContext _context;

        public KategorijaController(ApplicationDbContext dbContext)
        {
                _context= dbContext;
        }

        public IActionResult Index()
        {
            return View(_context.Kategorija.ToList());
        }

        public IActionResult Create ()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Kategorija kategorija)
        {

            if (ModelState.IsValid)
            {
                _context.Kategorija.Add(kategorija);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(kategorija);
        }

    }
}
