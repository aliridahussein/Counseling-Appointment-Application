using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CounsellingWebApplication.Models
{
    public interface IBookRepository
    {
        Book Add(Book book);
        Book Delete(string Email);
        IEnumerable<Book> GetAllBooks();
        Book Update(Book bookChanges);
        Book GetBook(string id);
    }
}
