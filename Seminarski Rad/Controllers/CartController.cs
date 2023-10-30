using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Seminarski_Rad.Data;
using Seminarski_Rad.Extensions;
using Seminarski_Rad.Models;

namespace Seminarski_Rad.Controllers
{
    public class CartController : Controller
    {
        private readonly string _sesijaKeyName = "_cart";
        private ApplicationDbContext _context;

        public CartController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sesijaKeyName);
            if (cart == null) cart = new List<CartItem>();

            decimal sum = 0;
            cart.Sum(item => sum += item.GetTotal());
            ViewBag.TotalPrice = sum;

            return View(cart);

        }


        [HttpPost]
        public IActionResult DodajCart(int productId)
        {

            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sesijaKeyName);
            if (cart == null) cart = new List<CartItem>();
            if (cart.Count == 0)
            {
                var proizvod = _context.Proizvod.FirstOrDefault(p => p.Id == productId);
                CartItem cartItem = new CartItem()
                {
                    Proizvod = proizvod,
                    Količina = 1
                };
                cart.Add(cartItem);
                HttpContext.Session.SetObjectAsJson(_sesijaKeyName, cart);
            }
            else
            {
                int result = PostojiuCart(productId);
                //2.
                // ako postoji u košarici, 
                if (result == -1)
                {
                    var product = _context.Proizvod.FirstOrDefault(p => p.Id == productId);

                    CartItem cartItem = new CartItem()
                    {
                        Proizvod = product,
                        Količina = 1
                    };
                    // dodati na postojeću listu
                    cart.Add(cartItem);
                }
                else
                {
                    cart[result].Količina++;
                }

                // dodati ponovo u sesiju
                HttpContext.Session.SetObjectAsJson(_sesijaKeyName, cart);
            }
            


            return RedirectToAction(nameof(Index));
        }

        private int PostojiuCart(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sesijaKeyName);
            if (cart == null) cart = new List<CartItem>();
                    
            for (int i = 0; i < cart.Count; i++)
            {
                if (cart[i].Proizvod.Id == productId)
                {
                    return i;
                }
            }

            return -1;
        }
        public IActionResult Delete(int productId)
        {
            List<CartItem> cart = HttpContext.Session.GetObjectAsJson<List<CartItem>>(_sesijaKeyName);
            if (cart == null) cart = new List<CartItem>();
            int result = PostojiuCart(productId);
            cart.RemoveAt(result);

            HttpContext.Session.SetObjectAsJson(_sesijaKeyName, cart);

            return RedirectToAction(nameof(Index));

        }
    }
}
