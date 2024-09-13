using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Repositories;

namespace LibraryManagementSystem.Service.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _bookRepository;

    //this should be a constant and not private.  should be on a static class that is public
    //so you can use this value when writing unit tests
    private readonly int _maxLoanDays = 14;

    public BookService(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    
    public void AddBook(BookDto bookDto)
    {
        _bookRepository.Add(bookDto);
    }
    
    public IEnumerable<BookDto> GetAllBooks()
    {
        return _bookRepository.GetAll();
    }
    
    public BookDto? GetBookById(Guid id)
    {
        return _bookRepository.GetById(id);
    }
    
    public void RemoveBook(Guid id)
    {
        var book = _bookRepository.GetById(id);
        if (book == null)
            throw new InvalidOperationException("Book is not available.");
        book.IsDeleted = true;
    }
    
    public IEnumerable<BookDto> GetAllOverDueBooks() =>
            _bookRepository.GetAll().Where(b => b.IsCheckedOut && b.ReturnDate <= DateTime.Now);

    public IEnumerable<BookDto> GetAllAvailableBooks() =>
          _bookRepository.GetAll().Where(b => !b.IsCheckedOut);

    public void CheckOutBook(Guid bookId)
    {
        var book = _bookRepository.GetById(bookId);
        if (book == null || book.IsCheckedOut)
            throw new InvalidOperationException("Book is not available.");

        book.IssueDate = DateTime.Now;
        book.ReturnDate = DateTime.Now.AddDays(_maxLoanDays);
        book.IsCheckedOut = true;
    }

    public void ReturnBook(Guid bookId)
    {
        var book = _bookRepository.GetById(bookId);

        if (book == null)
            throw new InvalidOperationException("Invalid return operation.");

        book.IsCheckedOut = false;
        book.ReturnDate = DateTime.Now;
    }

    public decimal CalculateLateFees(Guid bookId)
    {
        var book = _bookRepository.GetById(bookId);
        if (book == null || book.IsCheckedOut)
            throw new InvalidOperationException("Book has not been returned yet.");;
        var daysLate = (DateTime.Now - book.ReturnDate.Value).Days - _maxLoanDays;
        return daysLate > 0 ? daysLate * 10 : 0;
    }
}









