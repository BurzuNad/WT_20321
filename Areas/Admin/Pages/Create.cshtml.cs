using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using VT_20321.Data;
using VT_20321.Data.Catalog;

namespace VT_20321.Areas.Admin
{
    public class CreateModel : PageModel
    {
        private readonly VT_20321.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public CreateModel(VT_20321.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public IActionResult OnGet()
        {
        ViewData["CatalogCategoryId"] = new SelectList(_context.CatalogCategories, "CatalogCategoryId", "CatalogCategoryName");
            return Page();
        }

        [BindProperty]
        public CatalogItem CatalogItem { get; set; } /*= default!;*/
        
        [BindProperty]
        public IFormFile Image { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid || _context.CatalogItems == null || CatalogItem == null)
            {
                _context.CatalogItems.Add(CatalogItem);
                await _context.SaveChangesAsync();
                if (Image != null)
                {
                    var fileName = $"{CatalogItem.CatalogCategoryId}" +
                    Path.GetExtension(Image.FileName);
                    CatalogItem.Image = fileName;
                    var path = Path.Combine(_environment.WebRootPath, "images", fileName);
                    using (var fStream = new FileStream(path, FileMode.Create))
                    {
                        await Image.CopyToAsync(fStream);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToPage("./Index");
                
            }

            else { return Page(); }
        }
    }
}
