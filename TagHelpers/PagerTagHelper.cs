using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;

namespace VT_20321.TagHelpers {
    public class PagerTagHelper: TagHelper {
        LinkGenerator _linkGenerator;        
        public int PageCurrent { get; set; }  // номер текущей страницы        
        public int PageTotal { get; set; }  // общее количество страниц        
        public string PagerClass { get; set; }  // дополнительный css класс пейджера        
        public string Action { get; set; }  // имя action       
        public string Controller { get; set; }   // имя контроллера
        public int? CategoryId { get; set; }
        public PagerTagHelper(LinkGenerator linkGenerator) {
            _linkGenerator = linkGenerator;
        }
        public override void Process(TagHelperContext context, TagHelperOutput output) {            
            output.TagName = "nav";  // контейнер разметки пейджера            
            var ulTag = new TagBuilder("ul");  // пейджер
            ulTag.AddCssClass("pagination");
            ulTag.AddCssClass(PagerClass);
            for (int i = 1; i <= PageTotal; i++) {
                var url = _linkGenerator.GetPathByAction(Action, Controller,
                new {
                    pageNo = i,
                    category = CategoryId == 0
                ? null
                : CategoryId
                });
                // получение разметки одной кнопки пейджера
                var item = GetPagerItem(
                url: url, text: i.ToString(),
                active: i == PageCurrent,
                disabled: i == PageCurrent
                );                
                ulTag.InnerHtml.AppendHtml(item);  // добавить кнопку в разметку пейджера
            }            
            output.Content.AppendHtml(ulTag);  // добавить пейджер в контейнер
        }
        /// Генерирует разметку одной кнопки пейджера
        /// <param name="url">адрес</param>
        /// <param name="text">текст кнопки пейджера</param>
        /// <param name="active">признак текущей страницы</param>
        /// <param name="disabled">запретить доступ к кнопке</param>
        /// <returns>объект класса TagBuilder</returns>
        private TagBuilder GetPagerItem(string url, string text, bool active = false, bool disabled = false) {
            var liTag = new TagBuilder("li");  // создать тэг <li>
            liTag.AddCssClass("page-item");
            liTag.AddCssClass(active ? "active" : "");
            //liTag.AddCssClass(disabled ? "disabled" : "");            
            var aTag = new TagBuilder("a");  // создать тэг <a>
            aTag.AddCssClass("page-link");
            aTag.Attributes.Add("href", url);
            aTag.InnerHtml.Append(text);            
            liTag.InnerHtml.AppendHtml(aTag); // добавить тэг <a> внутрь <li>
            return liTag;
        }
    }
}
