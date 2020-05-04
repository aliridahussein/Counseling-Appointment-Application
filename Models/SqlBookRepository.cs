using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public class SqlBookRepository:IBookRepository
    {
        private readonly AppDbContext context;

        public SqlBookRepository(AppDbContext context)
        {
            this.context = context;
        }
        public Book Add(Book book)
        {
            context.books.Add(book);
            context.SaveChanges();
            return book;
        }
        public Book Delete(string Email)
        {
            Book book = context.books.Find(Email);
            if (book != null)
            {
                context.books.Remove(book);
                context.SaveChanges();
            }
            return book;

        }
        public IEnumerable<Book> GetAllBooks()
        {
            return context.books;
        }
        
        public Book Update(Book bookChanges)
        {
            var book = context.Attach(bookChanges);
            book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return bookChanges;
        }

        public Book GetBook(string id)
        {
            return context.books.Find(id);

        }


    }
}
