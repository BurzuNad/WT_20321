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
    public class DeleteModel : PageModel
    {
        private readonly VT_20321.Data.ApplicationDbContext _context;

        public DeleteModel(VT_20321.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.CatalogItems == null)
            {
                return NotFound();
            }
            var catalogitem = await _context.CatalogItems.FindAsync(id);

            if (catalogitem != null)
            {
                CatalogItem = catalogitem;
                _context.CatalogItems.Remove(CatalogItem);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
