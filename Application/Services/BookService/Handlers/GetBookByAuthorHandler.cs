using AutoMapper;
using Core.Interfaces.Repositories;
using Application.Queries;
using Application.Responses;
using MediatR;
using Core.ViewModels;
using Core.Entities;
using Application.Services.BookService.Responses;

namespace Application.Handlers
{
    public class GetBookByAuthorHandler : IRequestHandler<GetBooksByAuthorQuery, ApplicationResult<PaginationInfo<BookDto>>>
    {

        private readonly IBooksRepository _booksRepository;
        private readonly IGenericRepository<Author> _authorsRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookByAuthorHandler(IBooksRepository booksRepository, IMapper mapper, IUnitOfWork unitOfWork, 
            IGenericRepository<Book> repository, IGenericRepository<Author> authorsRepository)
        {
            _booksRepository = booksRepository;
            _authorsRepository = authorsRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ApplicationResult<PaginationInfo<BookDto>>> Handle(GetBooksByAuthorQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var booklist = await _booksRepository.GetBooksByAuthor(request.Id, request.pageNumber, request.perPage);
                var mapped = _mapper.Map<List<BookDto>>(booklist.Data);

                for (int i = 0; i < booklist.Data.Count; i++)
                {
                    var author = await _authorsRepository.GetAsync(booklist.Data[i].AuthorId);
                    mapped[i].Author = author.Name;
                }
                var pageAmount = Math.Ceiling((float)booklist.Count / (float)request.perPage);

                PaginationInfo<BookDto> pagination = new PaginationInfo<BookDto>
                {
                    Count = (int)booklist.Count,
                    PagesCount = pageAmount != null ? (int)pageAmount : 0,
                    CurrentPage = request.pageNumber,
                    PaginationData = mapped
                };

                return pagination.Count != 0 ? new ApplicationResult<PaginationInfo<BookDto>>
                {
                    StatusCode = 200,
                    Message = "Data Fetched Succesfully",
                    Data = pagination
                } : new ApplicationResult<PaginationInfo<BookDto>>
                {
                    StatusCode = 404,
                    Message = "Not found",
                    Data = null
                };
            } catch (Exception ex)
            {
                return new ApplicationResult<PaginationInfo<BookDto>>
                {
                    StatusCode = 500,
                    Message = "Server Error",
                    Data = null
                };
            }
            
        }
    }
}
