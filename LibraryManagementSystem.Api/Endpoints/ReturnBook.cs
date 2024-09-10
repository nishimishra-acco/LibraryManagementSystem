using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class ReturnBook : Endpoint<RequestId, ResponseResult<BookDto>>
    {
        private readonly ILibraryService _bookService;

        public ReturnBook(ILibraryService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/return/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            _bookService.ReturnBook(req.Id);
            await SendAsync(new ResponseResult<BookDto> { Message = "Book return!" });
        }
    }
}
