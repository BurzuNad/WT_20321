namespace VT_20321.Models {
    public class ListViewModel<T> : List<T> where T : class  {
        public int CurrentPage { get; set; }  // номер текущей страницы
        public int TotalPages { get; set; }  //общее количество страниц

        private ListViewModel(IEnumerable<T> items, int total, int current) : base(items) {
            TotalPages = total;
            CurrentPage = current;
        }
        public static ListViewModel<T> GetModel(IEnumerable<T> list, int current, int itemsPerPage) {
            var items = list   // список объектов
                        .Skip((current - 1) * itemsPerPage)
                        .Take(itemsPerPage) // количество объектов на странице
                        .ToList();
            var total = (int)Math.Ceiling((double)list.Count() / itemsPerPage);
            return new ListViewModel<T>(items, total, current);
        }        
    }
}
