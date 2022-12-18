using Application.Services.BookService.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly ISender _senderMediator;

        public AuthorsController(IMediator senderMediator)
        {
            _senderMediator = senderMediator;
        }

        [HttpPost("AddAuthor")]
        public async Task<IActionResult> AddAuthor(AddAuthorCommand dto)
        {
            var r = Task.Run(() => _senderMediator.Send(dto));
            return Ok(r.Result);
        }

        [HttpDelete("DeleteAuthor")]
        public async Task<IActionResult> DeleteAuthor(int authorId)
        {
            var r = Task.Run(() => _senderMediator.Send(new DeleteAuthorCommand(authorId)));
            return Ok(r.Result);
        }

    }
}
