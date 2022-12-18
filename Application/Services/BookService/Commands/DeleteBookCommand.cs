using Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Commands
{
    public record DeleteBookCommand(int bookId) : IRequest<ApplicationResult<bool>>;
}
