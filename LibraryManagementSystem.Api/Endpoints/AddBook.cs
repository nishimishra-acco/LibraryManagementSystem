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
            //not sure why you are creating a new bookDto when you have one handed to you and passed in as the
            //method argument?
           
            _bookService.AddBook(req);
            await SendAsync(new Response<BookDto> { data = req, Message = "Book Added Successfully" });
        }
    }
}