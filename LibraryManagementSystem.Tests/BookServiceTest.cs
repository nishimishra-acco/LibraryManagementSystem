using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Repositories;
using LibraryManagementSystem.Service.Services;
using Moq;

namespace LibraryManagementSystem.Tests;

/// <summary>
/// the mocks are not being used correctly.  the Verifies should be in a dispose method
/// </summary>
public class BookServiceTest : IDisposable
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly IBookService _bookServiceMock;
    private readonly Guid _bookId;

    private List<BookDto> BookDtoList()
    {
        // this should be in another class, not in this test.  how could these be reused for other test classes?
        
        return [
            new()
            {
                Title = "Operating System",
                Author = "Albert",
                Description = "Coding",
                IsDeleted = false,
                IsCheckedOut = false,
                IssueDate = null,
                ReturnDate = null,
                Id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"),
                AddedOn = DateTime.Now
            },
            new()
            {
                Title = "Data Structure",
                Author = "Mark",
                Description = "Coding",
                IsDeleted = false,
                IsCheckedOut = false,
                IssueDate = null,
                ReturnDate = null,
                Id = new Guid("a834dee5-d026-4042-b426-db087e55c38f"),
                AddedOn = DateTime.Now
            },
            new()
            {
                Title = "Data Structure 1",
                Author = "Mark 1",
                Description = "Coding 1",
                IssueDate = null,
                ReturnDate = null,
                IsDeleted = true,
                IsCheckedOut = false,
                Id = new Guid("b69613b5-9f07-42a8-a437-0b4e186fffbe"),
                AddedOn = DateTime.Now
            },
            new()
            {
                Title = "Data Structure 2",
                Author = "Mark 2",
                Description = "Coding 2",
                IssueDate = DateTime.Now,
                ReturnDate = DateTime.Now.AddDays(-15),
                IsDeleted = false,
                IsCheckedOut = true,
                Id = new Guid("7992249b-8d15-4d11-9de0-92eb848b0b06"),
                AddedOn = DateTime.Now
            }
    ];

}
    public BookServiceTest()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookServiceMock = new BookService(_bookRepositoryMock.Object);
        _bookId = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5"); // Or a specific ID if needed

    }

    public void Dispose() => _bookRepositoryMock.VerifyAll();

    [Fact]
    public void AddBook()
    {
        var book = BookDtoList()[0];
        _bookServiceMock.AddBook(book);
        _bookRepositoryMock.Verify(repo => repo.Add(book), Times.Once);
    }

    [Fact]
    public void GetAllBooks()
    {
        _bookRepositoryMock.Setup(r => r.GetAll()).Returns(BookDtoList);

        var books = _bookServiceMock.GetAllBooks();

        //Assert
        Assert.NotEmpty(books);
        Assert.Equal(4, books.Count());
        Assert.Equal("Operating System", books.ToList()[0].Title);
        Assert.Equal("Albert", books.ToList()[0].Author);
        Assert.Equal("Coding", books.ToList()[0].Description);
    }
    
    [Fact]
    public void GetBookById_BookExist()
    {
        var book = BookDtoList()[0];

        _bookRepositoryMock.Setup(r => r.GetById(_bookId)).Returns(book);

        book = _bookServiceMock.GetBookById(book.Id);

        _bookRepositoryMock.Verify(r => r.GetById(_bookId), Times.Once);

        Assert.NotNull(book);
        Assert.Equal("Operating System", book?.Title);
    }
    [Fact]
    public void GetBookById_BookNotExist()
    {
        var book = BookDtoList()[0];

        _bookRepositoryMock.Setup(r => r.GetById(_bookId)).Returns((BookDto)null);

        // Act

        book = _bookServiceMock.GetBookById(_bookId);

        // Assert
        Assert.Null(book);
        _bookRepositoryMock.Verify(r => r.GetById(_bookId), Times.Once);
    }
    [Fact]
    public void RemoveBook_BookExist()
    {
        var book = BookDtoList()[0];

        _bookRepositoryMock.Setup(r => r.GetById(_bookId)).Returns(book);

        _bookServiceMock.RemoveBook(book.Id);

        _bookRepositoryMock.Verify(r => r.GetById(_bookId), Times.Once);

        //Assert
        Assert.Equal(true, book?.IsDeleted);
    }
    [Fact]
    public void RemoveBook_BookNotExist()
    {

        _bookRepositoryMock.Setup(repo => repo.GetById(_bookId)).Returns((BookDto)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _bookServiceMock.RemoveBook(_bookId));
        Assert.Equal("Book is not available.", exception.Message);
        _bookRepositoryMock.Verify(repo => repo.GetById(_bookId), Times.Once);
    }
    [Fact]
    public void GetAllOverDueBooks()
    {
        _bookRepositoryMock.Setup(r => r.GetAll()).Returns(BookDtoList);

        var books =_bookServiceMock.GetAllOverDueBooks();
        //Assert
        Assert.NotEmpty(books);
        Assert.Equal("Data Structure 2", books.ToList()[0].Title);
    }
    [Fact]
    public void GetAllAvailableBooks()
    {
        _bookRepositoryMock.Setup(r => r.GetAll()).Returns(BookDtoList);

        var books = _bookServiceMock.GetAllAvailableBooks();
        //Assert
        Assert.NotEmpty(books);
        Assert.Equal(3, books.Count());
    }
    [Fact]
    public void CheckOutBook_BookExist()
    {
        var book = BookDtoList()[0];
        _bookRepositoryMock.Setup(r => r.GetById(_bookId)).Returns(book);
        _bookServiceMock.CheckOutBook(book.Id);

        //Assert
        Assert.True(book.IsCheckedOut);
    }
    [Fact]
    public void CheckOutBook_BookNotExist()
    {
        _bookRepositoryMock.Setup(repo => repo.GetById(_bookId)).Returns((BookDto)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _bookServiceMock.CheckOutBook(_bookId));
        Assert.Equal("Book is not available.", exception.Message);
        _bookRepositoryMock.Verify(repo => repo.GetById(_bookId), Times.Once);
    }
    [Fact]
    public void CheckOutBook_BookIsAlreadyCheckedOut_ThrowsInvalidOperationException()
    {
        // Arrange
        var book = new BookDto
        {
            Id = _bookId,
            IsCheckedOut = true
        };
        _bookRepositoryMock.Setup(repo => repo.GetById(_bookId)).Returns(book);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _bookServiceMock.CheckOutBook(_bookId));
        Assert.Equal("Book is not available.", exception.Message);
        _bookRepositoryMock.Verify(repo => repo.GetById(_bookId), Times.Once);
    }
    [Fact]
    public void ReturnBook_BookExist()
    {
        var book = BookDtoList()[0];

        _bookRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(book);
        _bookServiceMock.ReturnBook(book.Id);

        //Assert
        Assert.False(book.IsCheckedOut);
    }
    [Fact]
    public void ReturnBook_BookNotExist()
    {
        _bookRepositoryMock.Setup(repo => repo.GetById(It.IsAny<Guid>())).Returns((BookDto)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _bookServiceMock.ReturnBook(It.IsAny<Guid>()));
        Assert.Equal("Invalid return operation.", exception.Message);
        _bookRepositoryMock.Verify(repo => repo.GetById(It.IsAny<Guid>()), Times.Once);
    }
    [Fact]
    public void CalculateLateFees()
    {
        var book = new BookDto()
        {
            Title = "Operating System",
            Id = _bookId,
            IsCheckedOut = false,
            ReturnDate = DateTime.Now.AddDays(-20)
        };
        _bookRepositoryMock.Setup(r => r.GetById(_bookId)).Returns(book);
        decimal lateFees = _bookServiceMock.CalculateLateFees(_bookId);

        //Assert
        Assert.Equal(60, lateFees);
    }
    [Fact]
    public void CalculateLateFees_BookNotExist()
    {
        _bookRepositoryMock.Setup(repo => repo.GetById(_bookId)).Returns((BookDto)null);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _bookServiceMock.CalculateLateFees(_bookId));
        Assert.Equal("Book has not been returned yet.", exception.Message);
        _bookRepositoryMock.Verify(repo => repo.GetById(_bookId), Times.Once);
    }
    [Fact]
    public void CalculateLateFees_BookNotReturn_ThrowsInvalidOperationException()
    {
        // Arrange
        var book = new BookDto
        {
            Id = _bookId,
            IsCheckedOut = true
        };
        _bookRepositoryMock.Setup(repo => repo.GetById(_bookId)).Returns(book);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => _bookServiceMock.CalculateLateFees(_bookId));
        Assert.Equal("Book has not been returned yet.", exception.Message);
        _bookRepositoryMock.Verify(repo => repo.GetById(_bookId), Times.Once);
    }
}