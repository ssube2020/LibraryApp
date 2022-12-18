using Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Commands
{
    public record AddBookCommand(int authorId, string header, string description) : IRequest<ApplicationResult<bool>>;

}
