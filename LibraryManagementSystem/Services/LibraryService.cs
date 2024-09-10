using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Repositories;

namespace LibraryManagementSystem.Service.Services;

public class LibraryService : ILibraryService
{
    private readonly IBookRepository _bookRepository;
    private readonly ILoanRepository _loanRepository;
    private readonly int _maxLoanDays = 14;

    public LibraryService(IBookRepository bookRepository, ILoanRepository loanRepository)
    {
        _bookRepository = bookRepository;
        _loanRepository = loanRepository;
    }

    public void AddBook(string title, string author, string description)
    {
        var book = new BookDto(title, author, description);
        _bookRepository.Add(book);
    }

    public void RemoveBook(Guid id)
    {
        _bookRepository.Remove(id);
    }

    public BookDto? GetBookById(Guid id)
    {
        return _bookRepository.GetById(id);
    }
    public IEnumerable<BookDto> GetAvailableBooks() =>
            _bookRepository.GetAll().Where(b => !b.IsCheckedOut);

    public void CheckOutBook(Guid bookId, int userId)
    {
        var book = _bookRepository.GetById(bookId);
        if (book == null || book.IsCheckedOut)
            throw new InvalidOperationException("Book is not available.");

        var loan = new LoanDto
        {
            BookId = bookId,
            UserId = userId,
            LoanDate = DateTime.UtcNow
        };

        _loanRepository.Add(loan);
        book.IsCheckedOut = true;
    }

    public void ReturnBook(Guid bookId)
    {
        var book = _bookRepository.GetById(bookId);
        var loan = _loanRepository.GetByBookId(bookId);

        if (book == null || loan == null)
            throw new InvalidOperationException("Invalid return operation.");

        book.IsCheckedOut = false;
        loan.ReturnDate = DateTime.UtcNow;
    }

    public decimal CalculateLateFees(Guid bookId)
    {
        var loan = _loanRepository.GetByBookId(bookId);
        if (loan == null || loan.ReturnDate == null)
            throw new InvalidOperationException("Book has not been returned yet.");

        var daysLate = (DateTime.UtcNow - loan.LoanDate).Days - _maxLoanDays;
        return daysLate > 0 ? daysLate * 0.50m : 0;
    }
}









