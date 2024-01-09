using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using VT_20321.Data;
using VT_20321.Data.Catalog;

namespace VT_20321.Areas.Admin
{
    public class DetailsModel : PageModel
    {
        private readonly VT_20321.Data.ApplicationDbContext _context;

        public DetailsModel(VT_20321.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public CatalogItem CatalogItem { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CatalogItems == null)
            {
                return NotFound();
            }

            CatalogItem = await _context.CatalogItems
                .Include(c => c.Category)  // Загрузите связанные данные Category
                .FirstOrDefaultAsync(m => m.Id == id);

            if (CatalogItem == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
