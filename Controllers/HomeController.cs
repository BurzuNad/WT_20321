//--------HomeController.cs--------//
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using VT_20321.Models;

namespace VT_20321.Controllers {
    public class HomeController : Controller  {
        private readonly ILogger<HomeController> _logger;
        private List<ListDemo> _listDemo;

        public HomeController(ILogger<HomeController> logger)  // Для отладки
        {
            _logger = logger;
            _listDemo = new List<ListDemo> {
                new ListDemo{ ListItemValue=1, ListItemText="Item 1"},
                new ListDemo{ ListItemValue=2, ListItemText="Item 2"},
                new ListDemo{ ListItemValue=3, ListItemText="Item 3"}
            };
        }
        public IActionResult Index()
        {
            ViewData["Text"] = "Лабораторная работа"; //Передача данных представлению
            ViewData["Lst"] = new SelectList(_listDemo, "ListItemValue", "ListItemText");
            return View();
        }
    }
    public class ListDemo
    {
        public int ListItemValue { get; set; }
        public string ListItemText { get; set; }
    }
}
