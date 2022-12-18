using Core.Entities;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    public interface IBooksRepository : IGenericRepository<Book>
    {
        Task<ApplicationResult<List<Book>>> GetBooksByAuthor(int authorId, int pageNumber, int perPage);
        //Task<ApplicationResult<bool>> InsertBook(int authId, string header, string description);
    }
}
