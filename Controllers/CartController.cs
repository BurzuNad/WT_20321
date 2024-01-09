using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VT_20321.Models;
using VT_20321.Data;

namespace VT_20321.Controllers {
    public class CartController : Controller {
        private Cart _cart;
        private ApplicationDbContext _context;
        public CartController(ApplicationDbContext context, Cart cart) {
            _cart = cart;
            _context = context;
        }
        public IActionResult Index() {
            return View(_cart.Items.Values);
        }
        [Authorize]
        public IActionResult Add(int id, string returnUrl) {
            var item = _context.CatalogItems.Find(id);
            if (item != null) {
                _cart.AddToCart(item);
            }
            return Redirect(returnUrl);
        }
        public IActionResult Delete(int id) {
            _cart.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}