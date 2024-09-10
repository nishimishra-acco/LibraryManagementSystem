using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class DeleteBook : Endpoint<BookDto, ResponseResult<BookDto>>
    {
        private readonly ILibraryService _bookService;
        public DeleteBook(ILibraryService bookService)
        {
            _bookService = bookService;
        }
        public override void Configure()
        {
            Delete("/books/{id}");
            AllowAnonymous();
        }
        public override async Task HandleAsync(BookDto book, CancellationToken ct)
        {
            _bookService.RemoveBook(book.Id);
            await SendAsync(new ResponseResult<BookDto> { Message = "Book removed successfully!" });
        }
    }
}
