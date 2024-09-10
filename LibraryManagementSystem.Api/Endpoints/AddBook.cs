using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class AddBook : Endpoint<BookDto, Response<BookDto>>
    {
        private readonly IBookService _bookService;

        public AddBook(IBookService bookService)
        {
            _bookService = bookService;
        }

        public override void Configure()
        {
            Post("/book");
            AllowAnonymous();
        }

        public override async Task HandleAsync(BookDto req, CancellationToken ct)
        {
            BookDto bookDto = new()
            {
                    Id = Guid.NewGuid(),
                    Title = req.Title,
                    Author = req.Author,
                    Description = req.Description,
             };
            _bookService.AddBook(bookDto);
            await SendAsync(new Response<BookDto> { data = bookDto, Message = "Book Added Successfully" });
        }
    }
}