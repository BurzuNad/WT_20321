using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VT_20321.Data;
using VT_20321.Data.Catalog;
using VT_20321.Models;

namespace VT_20321.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    
    public class CatalogApiController : ControllerBase {
        private readonly ApplicationDbContext _context; //  Внедрите контекст базы данных в контроллер
        public CatalogApiController(ApplicationDbContext context) {
            _context = context;  //  Внедрите контекст базы данных в контроллер
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CatalogCategory>>> GetCatalogCategories(int? category) {
            return await _context.CatalogCategories
                                 .Where(d => !category.HasValue || d.CatalogCategoryId.Equals(category.Value))
                                 .ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CatalogItem>> GetCatalogItem(int id) {
            var catalogItem = await _context.CatalogItems.FindAsync(id);
            if (catalogItem == null) {
                return NotFound();
            }
            return catalogItem;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCatalogItem(int id, CatalogItem catalogItem)
        {
            if (id != catalogItem.CatalogCategoryId) {
                return BadRequest();
            }
            _context.Entry(catalogItem).State = EntityState.Modified;
            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!CategoryItemExists(id)) {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<CatalogItem>> PostCatalogItem(CatalogItem catalogItem) {
            _context.CatalogItems.Add(catalogItem);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetCatalogItem", new { id = catalogItem.CatalogCategoryId }, catalogItem);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryItem(int id) {
            var catalogItem = await _context.CatalogItems.FindAsync(id);
            if (catalogItem == null) {
                return NotFound();
            }
            _context.CatalogItems.Remove(catalogItem);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool CategoryItemExists(int id) {
            return _context.CatalogItems.Any(e => e.CatalogCategoryId == id);
        }
    }
       
    public class CatalogController : Controller {              
        /*// Инициализация списков  
        List<CatalogItem> _catalogItems;
        List<CatalogCategory> _catalogCategories;*/
        
        ApplicationDbContext _context;
        int _pageSize;
        private ILogger _logger;
        
        public CatalogController(ApplicationDbContext context, ILogger<CatalogController> logger) {
            /*SetupData(); // Инициализация списков*/
            _pageSize = 3;
            _context = context;
            _logger = logger;
        }
        //добавьте атрибуты маршрутизации перед методом
        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo}")]
        
        public IActionResult GetCatalog(int? category, int pageNo) {
            /*return View(_catalogItems); // Инициализация списков*/
            var catalogItemsFiltered = _context.CatalogItems
                .Where(d => !category.HasValue || d.CatalogCategoryId == category.Value);
            // Поместить список во ViewData
            ViewData["Category"] = _context.CatalogCategories;
            ViewData["CurrentCategory"] = category ?? 0;
            return View(ListViewModel<CatalogItem>.GetModel(catalogItemsFiltered, pageNo, _pageSize));
        }
        /*// Инициализация списков
        private void SetupData() {
            _catalogCategories = new List<CatalogCategory>() {
                new CatalogCategory {CatalogCategoryId=1, CatalogCategoryName="Junior"},
                new CatalogCategory {CatalogCategoryId=2, CatalogCategoryName="Professional"}
            };
            _context.CatalogCategories.AddRange(_catalogCategories);
            _catalogItems = new List<CatalogItem> {
                new CatalogItem {Id=1, Name="HeadBoomJR2022", Technology="Graphene / Auxetic / графит",
                    Price =355, CatalogCategoryId=1, Image="HeadBoomJR2022.jpg"},
                new CatalogItem {Id=2, Name="HeadGrapheneJR202", Technology="графит/Graphene",
                    Price =288, CatalogCategoryId=1, Image="HeadGrapheneJR2022.jpg"},
                new CatalogItem {Id = 3, Name="HeadSpeedJR2022", Technology="графит/Graphene",
                    Price =304, CatalogCategoryId=1, Image="HeadSpeedJR2022.jpg"},
                new CatalogItem {Id=4, Name="HeadExtremeProf2022", Technology="графит/Graphene",
                    Price =590, CatalogCategoryId=2, Image="HeadExtremeProf2022.jpg"}
            };
            _context.CatalogItems.AddRange(_catalogItems);
            _context.SaveChanges();
        }*/
    }    
}   