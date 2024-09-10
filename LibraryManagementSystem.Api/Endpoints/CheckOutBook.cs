using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Endpoints
{
    public class CheckOutBook : Endpoint<RequestId, ResponseResult<BookDto>>
    {
        private readonly ILibraryService _bookService;

        public CheckOutBook(ILibraryService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/checkout/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            _bookService.CheckOutBook(req.Id, 0);
            await SendAsync(new ResponseResult<BookDto> { Message = "Book checked out successfully!" });
        }
    }
}