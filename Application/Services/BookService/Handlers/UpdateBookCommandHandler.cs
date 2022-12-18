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
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, ApplicationResult<bool>>
    {
        private readonly IGenericRepository<Book> _booksRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateBookCommandHandler(IGenericRepository<Book> booksRepository, IUnitOfWork unitOfWork)
        {
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ApplicationResult<bool>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if(String.IsNullOrEmpty(request.header) || String.IsNullOrEmpty(request.description))
            {
                return new ApplicationResult<bool>
                {
                    StatusCode = 400,
                    Message = "Bad Request",
                    Data = false
                };
            }
            try
            {
                //var book = _booksRepository.Query().Where(x => x.AuthorId == request.bookId).SingleOrDefault();
                var book = await _booksRepository.GetAsync(request.bookId);
                if (book != null)
                {
                    book.Title = request.header;   
                    book.Description = request.description;
                    await _unitOfWork.SaveChangesAsync();
                    return new ApplicationResult<bool>
                    {
                        StatusCode = 200,
                        Message = "Updated Succesfully",
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
