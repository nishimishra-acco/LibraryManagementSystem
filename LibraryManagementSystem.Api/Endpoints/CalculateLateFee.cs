using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class CalculateLateFee : Endpoint<IdRequest, Response<Decimal>>
    {
        private readonly IBookService _bookService;

        public CalculateLateFee(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books/latefee/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(IdRequest req, CancellationToken ct)
        {
            var lateFees = _bookService.CalculateLateFees(req.Id);
            await SendAsync(new Response<Decimal> { data = lateFees });
        }
    }
}
