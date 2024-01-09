using System.ComponentModel.DataAnnotations;
using VT_20321.Data.Catalog;

namespace VT_20321.Models
{
    public class Cart
    {
        public Dictionary<int, CartItem> Items { get; set; }
        public Cart()
        {
            Items = new Dictionary<int, CartItem>();
        }
        //количество объектов в корзине
        public int Count
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity);
            }
        }
        //стоимость
        public decimal Price
        {
            get
            {
                return Items.Sum(item => item.Value.Quantity * item.Value.CatalogItem.Price);
            }
        }
        //добавление в корзину
        public virtual void AddToCart(CatalogItem catalogItem)
        {
            // если объект есть в корзине , то увеличить количество
            if (Items.ContainsKey(catalogItem.Id))
                Items[catalogItem.Id].Quantity++;
            // иначе - добавить объект в корзину
            else Items.Add(catalogItem.Id, new CartItem
            {
                CatalogItem = catalogItem,
                Quantity = 1
            });
        }
        //Удалить объект из корзины
        public virtual void RemoveFromCart(int id)
        {
            Items.Remove(id);
        }
        // Очистить корзину
        public virtual void ClearAll()
        {
            Items.Clear();
        }
    }

    // Класc описывает одну позицию в корзине
    public class CartItem
    {
        public CatalogItem CatalogItem { get; set; }

        [Display(Name = "Количество")]
        public int Quantity { get; set; }
    }
}