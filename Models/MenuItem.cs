namespace VT_20321.Models {
    public class MenuItem { 
        public bool IsPage { get; set; } = false;  // является ли страницей или методом контроллера                         
        public string Area { get; set; } = "";  // имя области                                               
        public string Action { get; set; } = "";  // имя действия контроллера                                                 
        public string Controller { get; set; } = ""; // имя контроллера                                                    
        public string Page { get; set; } = "";  // имя страницы                                                
        public string Active { get; set; } = "";  // класс CSS для текущего пункта меню                                                  
        public string Text { get; set; } = "";  // текст надписи
    }
}
