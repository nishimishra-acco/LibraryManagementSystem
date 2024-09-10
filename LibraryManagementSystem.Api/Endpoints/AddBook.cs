using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class AddBook : Endpoint<BookDto, ResponseResult<BookDto>>
    {
        private readonly ILibraryService _bookService;

        public AddBook(ILibraryService bookService)
        {
            _bookService = bookService;
        }

        public override void Configure()
        {
            Post("/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BookDto req, CancellationToken ct)
        {
            _bookService.AddBook(req.Title, req.Author, req.Description);
            await SendAsync(new ResponseResult<BookDto> { Message = "Check Late fee Amount" });
        }
    }
}