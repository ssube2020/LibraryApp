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
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, ApplicationResult<bool>>
    {
        private readonly IGenericRepository<Book> _booksRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IGenericRepository<Book> booksRepository, IUnitOfWork unitOfWork)
        {
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var book = await _booksRepository.GetAsync(request.bookId);
                if (book != null)
                {
                    _booksRepository.Remove(request.bookId);
                    await _unitOfWork.SaveChangesAsync();
                    return new ApplicationResult<bool>
                    {
                        StatusCode = 200,
                        Message = "Deleted Succesfully",
                        Data = true
                    };
                }
                else
                {
                    return new ApplicationResult<bool>
                    {
                        StatusCode = 404,
                        Message = "Book not found",
                        Data = false
                    };
                }
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
