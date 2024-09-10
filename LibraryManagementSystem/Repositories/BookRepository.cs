
using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<BookDto> _books = new List<BookDto>();

        public BookDto GetById(Guid id) => _books.FirstOrDefault(b => b.Id == id && !b.IsDeleted);

        public void Add(BookDto book) => _books.Add(book);

        public IEnumerable<BookDto> GetAll() => _books.Where(b => !b.IsDeleted);

    }
}