using BookApp.Models;

namespace BookApp.Data
{
    public static class ApplicationContext
    {
        public static List<Book> Books { get; set; }

        //constructor
        static ApplicationContext()
        {
            Books = new List<Book>()
            { 
                // book class member 
                new Book(){Id=1,Title="Serenad",Price=150},
                new Book(){Id=2,Title="Aşk ve Mai",Price=120},
                new Book(){Id=3,Title="Körlük",Price=95}
            };
        }
    }
}
