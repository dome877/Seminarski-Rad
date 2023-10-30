using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NarudzbaController : Controller
    {
        private ApplicationDbContext _context;
        public NarudzbaController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            return View(_context.Narudzba.ToList());
        }
        public IActionResult Details(int id)
        {
            if (id == 0) return NotFound();

            var order = _context.Narudzba.FirstOrDefault(o => o.Id == id);

            if (order == null) return NotFound();

            order.NarudzbaItem = (
                from orderItem in _context.NarudzbaItem
                where orderItem.NarudzbaId == order.Id
                select new NarudzbaItem
                {
                    Id = orderItem.Id,
                    NarudzbaId = orderItem.NarudzbaId,
                    ProizvodId = orderItem.ProizvodId,
                    ProizvodNaziv = _context.Proizvod.SingleOrDefault(p => p.Id == orderItem.ProizvodId).Naziv,
                    Količina = orderItem.Količina,
                    Ukupno = orderItem.Ukupno,
                }
                ).ToList();

            return View(order);
        }


        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Narudzba narudzba = _context.Narudzba.FirstOrDefault(o => o.Id == id);

            if (narudzba == null) return NotFound();



            return View(narudzba);
        }

        [HttpPost]
        public IActionResult Edit(Narudzba narudzba)
        {
            if (ModelState.IsValid)
            {
                _context.Update(narudzba);
                _context.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(narudzba);
        }

        public IActionResult Delete(int id)
        {
            var narudzba = _context.Narudzba.FirstOrDefault(c => c.Id == id);

            if (narudzba == null)
            {
                return NotFound();
            }

            _context.Narudzba.Remove(narudzba);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }




        public IActionResult NewOrderItem(int orderId)
        {
            NarudzbaItem narudzbaItem = new NarudzbaItem()
            {
                NarudzbaId = orderId
            };

            ViewBag.Products = _context.Proizvod.Select(p =>
            new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Naziv

            }
            ).ToList();

            return View(narudzbaItem);
        }

        [HttpPost]
        public IActionResult NewOrderItem(NarudzbaItem narudzbaItem)
        {
            if (ModelState.IsValid)
            {
                // Check if there's already a NarudzbaItem with the same ProizvodId in the Narudzba
                var existingItem = _context.NarudzbaItem
                    .FirstOrDefault(item => item.NarudzbaId == narudzbaItem.NarudzbaId && item.ProizvodId == narudzbaItem.ProizvodId);

                if (existingItem != null)
                {
                    // Update Količina and calculate the new Ukupno
                    existingItem.Količina += narudzbaItem.Količina;
                    var existingproduct = _context.Proizvod.Single(p => p.Id == existingItem.ProizvodId);
                    existingItem.Ukupno = existingItem.Količina * existingproduct.Cijena;

                    _context.Update(existingItem); // Update the existing item
                }
                else
                {
                    var product = _context.Proizvod.Single(p => p.Id == narudzbaItem.ProizvodId);
                    narudzbaItem.Ukupno = narudzbaItem.Količina * product.Cijena;
                    _context.NarudzbaItem.Add(narudzbaItem);
                }

                // Calculate the new total amount for the corresponding Narudzba
                var narudzba = _context.Narudzba
                    .Include(n => n.NarudzbaItem)
                    .Single(n => n.Id == narudzbaItem.NarudzbaId);

                decimal newTotalAmount = narudzba.NarudzbaItem.Sum(item => item.Ukupno);

                narudzba.Ukupno = newTotalAmount; // Update the total amount
                _context.SaveChanges();

                TempData["SuccessAdd"] = "Uspješno ste dodali novi proizvod!";

                return RedirectToAction(nameof(Details), new { id = narudzbaItem.NarudzbaId });
            }

            // ModelState is not valid, return to the view with validation errors
            return View(narudzbaItem);
        }

        public IActionResult DeleteItem(int id)
        {
            var narudzbaItem = _context.NarudzbaItem.FirstOrDefault(c => c.Id == id);

            if (narudzbaItem == null)
            {
                return NotFound();
            }

            int narudzbaId = narudzbaItem.NarudzbaId; // Store the narudzba ID before deleting the item

            _context.NarudzbaItem.Remove(narudzbaItem);

            // Calculate the new total amount for the corresponding Narudzba


            var narudzba = _context.Narudzba.FirstOrDefault(c => c.Id == narudzbaId);

            decimal newTotalAmount = narudzba.NarudzbaItem.Sum(item => item.Ukupno);

            narudzba.Ukupno = newTotalAmount; // Update the total amount

            // Update the Narudzba model before saving changes
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = narudzbaId });
        }




    }
}


