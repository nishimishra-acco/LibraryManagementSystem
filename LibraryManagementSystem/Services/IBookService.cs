using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Services
{
    public interface IBookService
    {
        void AddBook(BookDto bookDto);
        void RemoveBook(Guid id);

        BookDto? GetBookById(Guid id);

        IEnumerable<BookDto> GetAllBooks();

        IEnumerable<BookDto> GetAllOverDueBooks();
        IEnumerable<BookDto> GetAllAvailableBooks();


        void CheckOutBook(Guid id);
        void ReturnBook(Guid id);
        
        decimal CalculateLateFees(Guid id);
    }
}