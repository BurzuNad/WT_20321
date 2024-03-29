﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VT_20321.Data;
using VT_20321.Data.Catalog;

namespace VT_20321.Areas.Admin
{
    public class EditModel : PageModel
    {
        private readonly VT_20321.Data.ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public EditModel(VT_20321.Data.ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public CatalogItem CatalogItem { get; set; } /*= default!;*/
        
        [BindProperty]
        public IFormFile Image { get; set; }
       
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.CatalogItems == null)
            {
                return NotFound();
            }

            var catalogitem =  await _context.CatalogItems.FirstOrDefaultAsync(m => m.Id == id);
            if (catalogitem == null)
            {
                return NotFound();
            }
            CatalogItem = catalogitem;
           ViewData["CatalogCategoryId"] = new SelectList(_context.CatalogCategories, "CatalogCategoryId", "CatalogCategoryName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                if (Image != null)
                {
                    var fileName = $"{CatalogItem.CatalogCategoryId}" + Path.GetExtension(Image.FileName);
                    CatalogItem.Image = fileName;
                    var path = Path.Combine(_environment.WebRootPath, "images", fileName);
                    using (var fStream = new FileStream(path, FileMode.Create)) {
                        await Image.CopyToAsync(fStream);
                    }
                }

                _context.Attach(CatalogItem).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
            }
                catch (DbUpdateConcurrencyException)
                {
                if (!CatalogItemExists(CatalogItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
                
            }
            else { return Page(); }
            
        }

        private bool CatalogItemExists(int id)
        {
          return (_context.CatalogItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
