using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using VT_20321.Models;
using VT_20321.Data.Catalog;
using VT_20321.Extensions;


namespace VT_20321.Services {
    public class CartService : Cart {
        private string sessionKey = "cart";

        [JsonIgnore]
        ISession Session { get; set; }
        public static Cart GetCart(IServiceProvider sp) {            
            var session = sp.GetRequiredService<IHttpContextAccessor>().HttpContext.Session; // получить объект сессии
            var cart = session?.Get<CartService>("cart") ?? new CartService();  // получить CartService из сессии или создать новый для возможности тестирования
            cart.Session = session;
            return cart;
        }
        public override void AddToCart(CatalogItem catalogItem) {
            base.AddToCart(catalogItem);
            Session?.Set<CartService>(sessionKey, this);
        }
        public override void RemoveFromCart(int id) {
            base.RemoveFromCart(id);
            Session?.Set<CartService>(sessionKey, this);
        }
        public override void ClearAll() {
            base.ClearAll();
            Session?.Set<CartService>(sessionKey, this);
        }
    }
}
