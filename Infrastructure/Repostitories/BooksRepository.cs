using Core.Entities;
using Core.Interfaces.Repositories;
using Core.ViewModels;
using Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repostitories
{
    public class BooksRepository : GenericRepository<Book>, IBooksRepository
    {
        private readonly IGenericRepository<Book> _repository;
        private readonly ApplicationDbContext _db;
        public BooksRepository(IGenericRepository<Book> repository, ApplicationDbContext db) : base(db)
        {
            _repository = repository;
            _db = db;
        }
        public async Task<ApplicationResult<List<Book>>> GetBooksByAuthor(int authorId, int pageNumber, int perPage)
        {
            if(authorId != null && authorId != 0)
            {
                var res = _db.Books != null ? _db.Books.Where(x => x.AuthorId == authorId).Skip((pageNumber - 1) * perPage).Take(perPage).ToList() : null;
                int allCount = res.Count != 0 ? _db.Books.Where(x => x.AuthorId == authorId).Count() : 0;
                return new ApplicationResult<List<Book>>
                {
                    Count = allCount,
                    StatusCode = res != null && res.Count != 0 ? 200 : 404,
                    Message = res != null && res.Count != 0 ? "success" : "Not Found",
                    Data = res
                };
            } else
            {
                var res = _db.Books != null ? _db.Books.Skip((pageNumber - 1) * perPage).Take(perPage).ToList() : null;
                int allCount = res.Count != 0 ? _db.Books.Count() : 0;
                return new ApplicationResult<List<Book>>
                {
                    Count = allCount,
                    StatusCode = res != null && res.Count != 0 ? 200 : 404,
                    Message = res != null && res.Count != 0 ? "success" : "Not Found",
                    Data = res
                };
            }
        }
    }
}
