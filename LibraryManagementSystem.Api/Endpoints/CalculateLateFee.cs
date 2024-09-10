using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class CalculateLateFee : Endpoint<RequestId, ResponseResult<BookDto>>
    {
        private readonly ILibraryService _bookService;

        public CalculateLateFee(ILibraryService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/latefee/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            _bookService.CalculateLateFees(req.Id);
            await SendAsync(new ResponseResult<BookDto> { Message = "Check Late fee Amount" });
        }
    }
}
