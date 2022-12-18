using Application.Services.BookService.Commands;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Handlers
{
    public class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand, ApplicationResult<bool>>
    {

        private readonly IGenericRepository<Author> _authorsRepository;
        private readonly IGenericRepository<Book> _booksRepository;
        private readonly IUnitOfWork _unitOfWork;
        public DeleteAuthorCommandHandler(IGenericRepository<Author> authorsRepository, IGenericRepository<Book> booksRepository, IUnitOfWork unitOfWork)
        {
            _authorsRepository = authorsRepository;
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _authorsRepository.GetAsync(request.authorId);
            if (author == null)
            {
                return new ApplicationResult<bool>
                {
                    StatusCode = 404,
                    Message = "Author Not Found",
                    Data = false
                };
            }
            try
            {
                _authorsRepository.Remove(request.authorId);
                var booksToDelete = _booksRepository.Query().Where(x => x.AuthorId == request.authorId).ToList();
                foreach(var itm in booksToDelete)
                {
                    _booksRepository.Remove(itm.Id);
                }
                await _unitOfWork.SaveChangesAsync();
                return new ApplicationResult<bool>
                {
                    StatusCode = 200,
                    Message = "Deleted Succesfully",
                    Data = true
                };
            }
            catch (Exception ex)
            {
                return new ApplicationResult<bool>
                {
                    StatusCode = 500,
                    Message = "Server Error",
                    Data = false
                };
            }

        }
    }
}
