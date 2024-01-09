using System.ComponentModel.DataAnnotations;

namespace VT_20321.Data.Catalog {
    public class CatalogItem {
        public int Id { get; set; }
        
        [Display(Name = "Наименование")]
        public string? Name { get; set; }
        
        [Display(Name = "Состав")]
        public string? Technology { get; set; }
       
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        
        [Display(Name = "Изображение")]
        public string? Image { get; set; }

        // Навигационные свойства
        public int CatalogCategoryId { get; set; }
        public CatalogCategory Category { get; set; }
    }
}
