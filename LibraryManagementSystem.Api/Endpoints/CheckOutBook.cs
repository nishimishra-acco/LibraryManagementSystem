using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Endpoints
{
    public class CheckOutBook : Endpoint<IdRequest, Response<BookDto>>
    {
        private readonly IBookService _bookService;

        public CheckOutBook(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Patch("/book/checkout");
            AllowAnonymous();
        }

        public override async Task HandleAsync(IdRequest req, CancellationToken ct)
        {
            try
            {
                _bookService.CheckOutBook(req.Id);
                await SendAsync(new Response<BookDto> { Message = "Book checked out successfully!" });
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                await SendAsync(new Response<BookDto>
                {
                    Message = ex.Message
                });
            }
        }
    }
}