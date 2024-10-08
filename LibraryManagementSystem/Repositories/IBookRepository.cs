﻿using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Repositories
{
    public interface IBookRepository
    {
        BookDto GetById(Guid id);
        void Add(BookDto book);
        IEnumerable<BookDto> GetAll();
    }
}