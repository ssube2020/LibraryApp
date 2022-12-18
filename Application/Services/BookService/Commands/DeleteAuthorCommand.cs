using Core.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Commands
{
    public record DeleteAuthorCommand(int authorId) : IRequest<ApplicationResult<bool>>;
}
