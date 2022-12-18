using Application.Queries;
using Application.Services.BookService.Commands;
using LibraryAppAPI.ApiDtoObjects;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly ISender _senderMediator;
        public BooksController(IMediator senderMediator)
        {
            _senderMediator = senderMediator;
        }

        [HttpGet("GetBooksPagination")]
        public async Task<IActionResult> GetBooks(int authorId, int pageNumber, int perPage)
        {
            return Ok(await _senderMediator.Send(new GetBooksByAuthorQuery(authorId, pageNumber, perPage)));
        }

        [HttpPost("AddBook")]
        public async Task<IActionResult> AddBook(AddBookCommand dto)
        {
            return Ok(await _senderMediator.Send(dto));
        }

        [HttpPut("UpdateBook")]
        public async Task<IActionResult> UpdateBook(UpdateBookCommand dto)
        {
            return Ok(await _senderMediator.Send(dto));
        }

        [HttpDelete("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int bookId)
        {
            return Ok(await _senderMediator.Send(new DeleteBookCommand(bookId)));
        }
    }
}