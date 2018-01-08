using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLINQ
{
    class Program
    {
        static void Main(string[] args)
        {
            DataClassDataContext db = new DataClassDataContext();

            var booksList = db.Books;
            foreach (Books book in booksList)
            {
                var categoryName = book.Category;

                //var departmentName = db.Books.SingleOrDefault(t => t.Category == 2);

                Console.WriteLine("Employee Title = {0} , Price = {1}, Id = {2}. Category = {3}",
                book.Title, book.Price, book.Id, categoryName);
            }


            Books newBook = new Books();
            newBook.Title = "C#. Praktyczny kurs";
            newBook.Price = 49;
            var category = from t in db.BookCategories
                           where t.Id == 2
                           select t.Id;

            newBook.Category = category.SingleOrDefault();

            db.Books.InsertOnSubmit(newBook);
            db.SubmitChanges();
            Books insertedBook = db.Books.FirstOrDefault(e => e.Title.Equals("C#. Praktyczny kurs"));
            Console.WriteLine("Employee Title = {0} , Price = {1}, Id = {2}. Category = {3}",
                           insertedBook.Title, insertedBook.Price, insertedBook.Id, insertedBook.Category);

            insertedBook.Price = 39;
            db.SubmitChanges();

            Books updatedBook = db.Books.FirstOrDefault(e => e.Title.Equals("C#. Praktyczny kurs"));
            Console.WriteLine("Employee Title = {0} , Price = {1}, Id = {2}. Category = {3}",
                        updatedBook.Title, updatedBook.Price, updatedBook.Id, updatedBook.Category);

            var rubyCategory = from t in db.BookCategories where t.Name == "LINQ" select t.Id;
            var idBooksToDelete = db.Books.Select(e => e).Where(e => e.Category.Equals(rubyCategory.SingleOrDefault()));

            //db.Books.DeleteOnSubmit(idBooksToDelete);
            //db.SubmitChanges();

            foreach (Books b in idBooksToDelete)
            {
                 db.Books.DeleteOnSubmit(b);
                 db.SubmitChanges();
            }

            var booksListWithoutRuby = db.Books;
            foreach (Books book in booksListWithoutRuby)
            {
                var categoryName = book.Category;

                //var departmentName = db.Books.SingleOrDefault(t => t.Category == 2);

                Console.WriteLine("Employee Title = {0} , Price = {1}, Id = {2}. Category = {3}",
                book.Title, book.Price, book.Id, categoryName);
            }

            Console.ReadKey();
        }
    }
}
