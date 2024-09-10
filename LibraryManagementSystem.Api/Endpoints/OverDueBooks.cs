using FastEndpoints;
using LibraryManagementSystem.Api.Endpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Endpoints
{
    public class OverDueBooks : Endpoint<IdRequest, Response<IEnumerable<BookDto>>>
    {
        private readonly IBookService _bookService;

        public OverDueBooks(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/OverDueBooks");
            AllowAnonymous();
        }

        public override async Task HandleAsync(IdRequest req, CancellationToken ct)
        {
            try
            {
                var books = _bookService.GetAllOverDueBooks();
                await SendAsync(new Response<IEnumerable<BookDto>> { data = books });
            }
            catch (Exception ex)
            {
                // Handle other potential exceptions
                await SendAsync(new Response<IEnumerable<BookDto>>
                {
                    Message = ex.Message
                });
            }
        }
    }
}