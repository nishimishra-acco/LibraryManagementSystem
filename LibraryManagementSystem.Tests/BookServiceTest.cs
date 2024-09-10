using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Repositories;
using LibraryManagementSystem.Service.Services;
using Moq;
using System.Collections.Generic;

namespace LibraryManagementSystem.Tests;

public class BookServiceTest
{
    private readonly Mock<IBookRepository> _bookRepositoryMock;
    private readonly IBookService _bookServiceMock;
    private readonly List<BookDto> books =
    [
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
    public BookServiceTest()
    {
        _bookRepositoryMock = new Mock<IBookRepository>();
        _bookRepositoryMock.Setup(r => r.GetAll()).Returns(books);
        _bookServiceMock = new BookService(_bookRepositoryMock.Object);
    }

    [Fact]
    public void AddBook()
    {
        Guid id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5");
        var book = new BookDto()
        {
            Title = "Operating System",
            Author = "Albert",
            Description = "Coding",
            Id = id
        };
        _bookServiceMock.AddBook(book);

        _bookRepositoryMock.Verify(repo => repo.Add(book), Times.Once);
    }

    [Fact]
    public void GetAllBooks()
    {
        var books = _bookServiceMock.GetAllBooks();

        //Assert
        Assert.NotEmpty(books);
        Assert.Equal(4, books.Count());
        Assert.Equal("Operating System", books.ToList()[0].Title);
        Assert.Equal("Albert", books.ToList()[0].Author);
        Assert.Equal("Coding", books.ToList()[0].Description);
    }
    [Fact]
    public void GetBookById()
    {
        Guid id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5");
        var book = new BookDto()
        {
            Title = "Operating System",
            Id = id,
        };
        _bookRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(book);
        book = _bookServiceMock.GetBookById(id);

        _bookRepositoryMock.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Once);

        Assert.NotNull(book);
        Assert.Equal("Operating System", book?.Title);
    }
    [Fact]
    public void RemoveBook()
    {
        Guid id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5");
        var book = new BookDto()
        {
            Title = "Operating System",
            Id = id,
        };
        _bookRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(book);
        _bookServiceMock.RemoveBook(id);

        _bookRepositoryMock.Verify(r => r.GetById(It.IsAny<Guid>()), Times.Once);

        //Assert
        Assert.Equal(true, book?.IsDeleted);
    }
    [Fact]
    public void GetAllOverDueBooks()
    {
        var books =_bookServiceMock.GetAllOverDueBooks();
        //Assert
        Assert.NotEmpty(books);
        Assert.Equal("Data Structure 2", books.ToList()[0].Title);
    }
    [Fact]
    public void CheckOutBook()
    {
        Guid id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5");
        var book = new BookDto()
        {
            Title = "Operating System",
            Id = id
        };
        _bookRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(book);
        _bookServiceMock.CheckOutBook(id);

        //Assert
        Assert.True(book.IsCheckedOut);
    }
    [Fact]
    public void ReturnBook()
    {
        Guid id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5");
        var book = new BookDto()
        {
            Title = "Operating System",
            Id = id,
            IsCheckedOut = true,
        };
        _bookRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(book);
        _bookServiceMock.ReturnBook(id);

        //Assert
        Assert.False(book.IsCheckedOut);
    }
    [Fact]
    public void CalculateLateFees()
    {
        Guid id = new Guid("8279e90d-b9e5-478e-b499-904bb43d38e5");
        var book = new BookDto()
        {
            Title = "Operating System",
            Id = id,
            IsCheckedOut = false,
            ReturnDate = DateTime.Now.AddDays(-20)
        };
        _bookRepositoryMock.Setup(r => r.GetById(It.IsAny<Guid>())).Returns(book);
        decimal lateFees = _bookServiceMock.CalculateLateFees(id);

        //Assert
        Assert.Equal(60, lateFees);
    }
}