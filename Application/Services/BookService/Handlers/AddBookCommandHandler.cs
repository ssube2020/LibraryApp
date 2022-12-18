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
    public class AddBookCommandHandler : IRequestHandler<AddBookCommand, ApplicationResult<bool>>
    {
        private readonly IGenericRepository<Book> _booksRepository;
        private readonly IGenericRepository<Author> _authorRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AddBookCommandHandler(IGenericRepository<Book> booksRepository, IGenericRepository<Author> authorRepository, IUnitOfWork unitOfWork)
        {
            _authorRepository = authorRepository;
            _booksRepository = booksRepository;
            _unitOfWork = unitOfWork;   
        }

        public async Task<ApplicationResult<bool>> Handle(AddBookCommand request, CancellationToken cancellationToken)
        {
            try
            {
                if (await _authorRepository.GetAsync(request.authorId) != null)
                {
                    var res = await _booksRepository.InsertAndReturnAsync(
                    new Book
                    {
                        AuthorId = request.authorId,
                        Title = request.header,
                        Description = request.description,
                    });
                    await _unitOfWork.SaveChangesAsync();
                    return  new ApplicationResult<bool>
                    {
                        StatusCode = 200,
                        Message = "Book Added Succesfully",
                        Data = true
                    };
                }
                else
                {
                    return new ApplicationResult<bool>
                    {
                        StatusCode = 404,
                        Message = "Author Not Found",
                        Data = false
                    };
                }
            } catch (Exception ex)
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
