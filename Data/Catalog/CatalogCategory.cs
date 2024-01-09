using System.ComponentModel.DataAnnotations;

namespace VT_20321.Data.Catalog {
    public class CatalogCategory {
        public int CatalogCategoryId { get; set; }

        [Display(Name = "Категория")]
        public string? CatalogCategoryName { get; set; }

        // Навигационное свойство 1-ко-многим
        public List<CatalogItem> CatalogItems { get; set; }
    }
}
