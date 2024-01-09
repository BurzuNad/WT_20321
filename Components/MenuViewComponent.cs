using Microsoft.AspNetCore.Mvc;
using VT_20321.Models;

namespace VT_20321.Components {
    public class MenuViewComponent: ViewComponent {
        // Инициализация списка элементов меню
        private List<MenuItem> _menuItems = new List<MenuItem> {
            new MenuItem {Controller="Home", Action="Index", Text="Home"},
            new MenuItem {Controller="Catalog", Action="Index", Text="Каталог"},
            new MenuItem {IsPage=true, Area="Admin", Page="/Index", Text="Администрирование"}
        };
        public IViewComponentResult Invoke() {
            //Получение значений сегментов маршрута
            var controller = ViewContext.RouteData.Values["controller"];
            var page = ViewContext.RouteData.Values["page"];
            var area = ViewContext.RouteData.Values["area"];
            foreach (var item in _menuItems) {
                var _matchController = controller?.Equals(item.Controller) ?? false;  // Название контроллера совпадает?
                var _matchArea = area?.Equals(item.Area) ?? false;   // Название зоны совпадает?
                // Если есть совпадение, то сделать элемент меню активным (применить соответствующий класс CSS)
                if (_matchController || _matchArea) {
                    item.Active = "active";
                }
            }
            return View(_menuItems);
        }
    }
}
