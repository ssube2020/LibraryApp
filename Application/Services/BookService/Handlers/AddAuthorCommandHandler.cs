using Application.Services.BookService.Commands;
using Core.Entities;
using Core.Interfaces.Repositories;
using Core.ViewModels;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Handlers
{
    public class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, ApplicationResult<bool>>
    {
        private readonly IGenericRepository<Author> _repository;
        private readonly IUnitOfWork _unitOfWork;
        public AddAuthorCommandHandler(IGenericRepository<Author> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApplicationResult<bool>> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
        {
            if(String.IsNullOrEmpty(request.Name) || String.IsNullOrEmpty(request.Surname))
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
                
                var res = _repository.AddAsync(new Author { Name = request.Name, Surname = request.Surname });
                //var res = await _repository.InsertAndReturnAsync(new Author { Name = request.Name, Surname = request.Surname });
                await _unitOfWork.SaveChangesAsync();
                return new ApplicationResult<bool>
                {
                    StatusCode = 200,
                    Message = "Added Succesfully",
                    Data = true
                };

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
