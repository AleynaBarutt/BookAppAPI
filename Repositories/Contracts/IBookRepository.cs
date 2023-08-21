using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Contracts
{
    public interface IBookRepository : IRepositoryBase<Book>
    {
        // baseden gelen metotlar hariç book için ekstra metot
        //yazarsak buraya eklenir.
        IQueryable<Book> GetAllBooks(bool  trackChanges);
        Book GetOneBookById(int id, bool  trackChanges);
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
        
    }
}
