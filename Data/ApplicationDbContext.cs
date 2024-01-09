using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VT_20321.Data.Catalog;
using VT_20321.Models;

namespace VT_20321.Data {
    public class ApplicationDbContext : IdentityDbContext <ApplicationUser> {
        public DbSet<CatalogItem> CatalogItems { get; set; }
        public DbSet<CatalogCategory> CatalogCategories { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {       
        
        }
    }
}