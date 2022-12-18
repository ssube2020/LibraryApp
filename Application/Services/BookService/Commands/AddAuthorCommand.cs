using Core.ViewModels;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.BookService.Commands
{
    public record AddAuthorCommand(string Name, string Surname) : IRequest<ApplicationResult<bool>>;
}
