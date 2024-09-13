using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;


namespace LibraryManagementSystem.Api.Endpoints
{
    internal class GetBook : Endpoint<IdRequest, Response<BookDto>>
    {
        private readonly IBookService _bookService;

        public GetBook(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/book/{id}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(IdRequest req, CancellationToken ct)
        {
            BookDto bookDto = _bookService.GetBookById(req.Id);
            // why you not check this inside the service?   
            // everywhere in the code when you need to get book you are going to check for nulls?
            //
            if (bookDto == null)
            {
                await SendAsync(new Response<BookDto> { Message = $"Book with id {req.Id} not found" });
                return;
                
            }
            await SendAsync(new Response<BookDto> { data = bookDto });
        }
    }
}
