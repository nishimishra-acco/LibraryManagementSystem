using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Services
{
    public interface ILibraryService
    {
        void AddBook(string title, string author, string Description);
        void RemoveBook(Guid id);

        BookDto? GetBookById(Guid id);

        IEnumerable<BookDto> GetAvailableBooks();

        void CheckOutBook(Guid id, int userId);
        void ReturnBook(Guid id);
        decimal CalculateLateFees(Guid id);
    }
}