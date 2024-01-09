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
    public class IndexModel : PageModel
    {
        private readonly VT_20321.Data.ApplicationDbContext _context;

        public IndexModel(VT_20321.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<CatalogItem> CatalogItem { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.CatalogItems != null)
            {
                CatalogItem = await _context.CatalogItems
                .Include(c => c.Category).ToListAsync();
            }
        }
    }
}
