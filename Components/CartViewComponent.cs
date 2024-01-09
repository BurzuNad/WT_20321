using Microsoft.AspNetCore.Mvc;
using VT_20321.Models;
/*using VT_20321.Data.Catalog;*/

namespace VT_20321.Components {
    public class CartViewComponent : ViewComponent {
        private Cart _cart;
        public CartViewComponent(Cart cart) {
            _cart = cart;
        }
        public IViewComponentResult Invoke()  {
            return View(_cart);
        }
    }
}
