
using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Repositories
{
    public class InMemoryBookRepository : IBookRepository
    {
        private readonly List<BookDto> _books = new List<BookDto>();

        public BookDto GetById(Guid id) => _books.FirstOrDefault(b => b.Id == id);

        public void Add(BookDto book) => _books.Add(book);

        public void Remove(Guid id) => _books.RemoveAll(b => b.Id == id);

        public IEnumerable<BookDto> GetAll() => _books;

    }
}