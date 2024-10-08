﻿using FastEndpoints;
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Services;

namespace LibraryManagementSystem.Api.Endpoints
{
    public class DeleteBook : Endpoint<BookDto, Response<BookDto>>
    {
        private readonly IBookService _bookService;
        public DeleteBook(IBookService bookService)
        {
            _bookService = bookService;
        }
        public override void Configure()
        {
            Delete("/book/{id}");
            AllowAnonymous();
        }
        public override async Task HandleAsync(BookDto book, CancellationToken ct)
        {
            try
            {
                _bookService.RemoveBook(book.Id);
                await SendAsync(new Response<BookDto> { Message = "Book removed successfully!" });
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
