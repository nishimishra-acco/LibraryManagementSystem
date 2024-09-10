using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;


namespace LibraryManagementSystem.Api.Endpoints
{
    internal class GetBook : Endpoint<RequestId, ResponseResult<BookDto>>
    {
        private readonly ILibraryService _bookService;

        public GetBook(ILibraryService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(RequestId req, CancellationToken ct)
        {
            BookDto bookDto = _bookService.GetBookById(req.Id);
            await SendAsync(new ResponseResult<BookDto> { bookDto = bookDto, Message = "Book found successfully!" });
        }
    }
}
