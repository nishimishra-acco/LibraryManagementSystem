
using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly List<BookDto> _books = new List<BookDto>();

        // from a logical stand-point if you are calling this method should you not expect to get an object?
        // if it does not exist I would expect an exception to be thrown here.
        
        public BookDto GetById(Guid id) => _books.FirstOrDefault(b => b.Id == id && !b.IsDeleted);

        public void Add(BookDto book) => _books.Add(book);

        public IEnumerable<BookDto> GetAll() => _books.Where(b => !b.IsDeleted);

    }
}