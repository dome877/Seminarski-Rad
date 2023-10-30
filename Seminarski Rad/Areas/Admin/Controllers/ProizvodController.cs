using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProizvodController : Controller
    {
        
        private ApplicationDbContext _context;

        public ProizvodController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            return View(_context.Proizvod.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Proizvod proizvod)
        {

            if (ModelState.IsValid)
            {
                _context.Proizvod.Add(proizvod);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            return View(proizvod);
        }

        public IActionResult Delete(int id)
        {
            var proizvod = _context.Proizvod.FirstOrDefault(c => c.Id == id);

            if (proizvod == null)
            {
                return NotFound();
            }

            _context.Proizvod.Remove(proizvod);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var proizvod = _context.Proizvod.FirstOrDefault(k => k.Id == id);

            if (proizvod == null)
            {
                return NotFound();
            }

            return View(proizvod);
        }

        [HttpPost]
        public IActionResult Edit(Proizvod proizvod)
        {
            if (ModelState.IsValid)
            {
                _context.Update(proizvod);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(proizvod);
        }

        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();

            var proizvod = _context.Proizvod.FirstOrDefault(p => p.Id == id);

            if (proizvod == null) return NotFound();

            return View(proizvod);
        }



    }
}
