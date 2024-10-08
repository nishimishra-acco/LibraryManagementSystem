﻿using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;


namespace LibraryManagementSystem.Api.Endpoints
{
    internal class GetAllBook : Endpoint<IdRequest, Response<IEnumerable<BookDto>>>
    {
        private readonly IBookService _bookService;

        public GetAllBook(IBookService bookService)
        {
            _bookService = bookService;
        }


        public override void Configure()
        {
            Get("/books");
            AllowAnonymous();
        }

        public override async Task HandleAsync(IdRequest req, CancellationToken ct)
        {
            IEnumerable<BookDto> bookDto = _bookService.GetAllBooks();
            await SendAsync(new Response<IEnumerable<BookDto>> { data = bookDto });
        }
    }
}
