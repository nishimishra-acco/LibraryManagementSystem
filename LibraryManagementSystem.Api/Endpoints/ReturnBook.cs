using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class ReturnBook : Endpoint<IdRequest, Response<BookDto>>
    {
        private readonly IBookService _bookService;

        public ReturnBook(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Patch("/book/return");
            AllowAnonymous();
        }

        public override async Task HandleAsync(IdRequest req, CancellationToken ct)
        {
            try
            {
                _bookService.ReturnBook(req.Id);
                await SendAsync(new Response<BookDto> { Message = "Book return!" });
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
