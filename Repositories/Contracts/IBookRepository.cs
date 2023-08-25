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
        Task<IEnumerable<Book>> GetAllBooksAsync(bool  trackChanges);
        Task<Book> GetOneBookByIdAsync(int id, bool  trackChanges);

        //tracking olduğu için async eklemeyiz.
        void CreateOneBook(Book book);
        void UpdateOneBook(Book book);
        void DeleteOneBook(Book book);
        
    }
}
