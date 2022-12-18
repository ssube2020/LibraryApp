using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.DataAccess;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repostitories
{
    internal class AuthorsRepository : GenericRepository<Author>, IAuthorsRepository
    {
        private readonly IGenericRepository<Author> _repository;
        private readonly ApplicationDbContext _db;

        public AuthorsRepository(IGenericRepository<Author> repository, ApplicationDbContext db) : base(db)
        {
            _repository = repository;
            _db = db;
        }
    }
}
