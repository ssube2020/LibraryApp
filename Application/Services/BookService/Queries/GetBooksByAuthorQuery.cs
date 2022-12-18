using Application.Responses;
using Application.Services.BookService.Responses;
using Core.ViewModels;
using MediatR;

namespace Application.Queries
{
    public record GetBooksByAuthorQuery(int Id, int pageNumber, int perPage) : IRequest<ApplicationResult<PaginationInfo<BookDto>>>;
}
