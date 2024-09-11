
using LibraryManagementSystem.Service.Models;
using LibraryManagementSystem.Service.Repositories;


namespace LibraryManagementSystem.Tests;


public class BookRespositoryTest
{
    private readonly BookRepository _repository;

    public BookRespositoryTest()
    {
        _repository = new BookRepository();
    }

    [Fact]
    public void GetById_ReturnsCorrectBook_WhenBookExists()
    {
        // Arrange
        var book = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "Test Book",
            IsDeleted = false
        };
        _repository.Add(book);

        // Act
        var result = _repository.GetById(book.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal(book.Title, result.Title);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenBookDoesNotExist()
    {
        // Arrange
        var nonExistentId = Guid.NewGuid();

        // Act
        var result = _repository.GetById(nonExistentId);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void GetById_ReturnsNull_WhenBookIsDeleted()
    {
        // Arrange
        var book = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "Test Book",
            IsDeleted = true
        };
        _repository.Add(book);

        // Act
        var result = _repository.GetById(book.Id);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Add_AddsBookToRepository()
    {
        // Arrange
        var book = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "New Book",
            IsDeleted = false
        };

        // Act
        _repository.Add(book);
        var result = _repository.GetById(book.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(book.Id, result.Id);
        Assert.Equal(book.Title, result.Title);
    }

    [Fact]
    public void GetAll_ReturnsOnlyNonDeletedBooks()
    {
        // Arrange
        var activeBook = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "Active Book",
            IsDeleted = false
        };
        var deletedBook = new BookDto
        {
            Id = Guid.NewGuid(),
            Title = "Deleted Book",
            IsDeleted = true
        };
        _repository.Add(activeBook);
        _repository.Add(deletedBook);

        // Act
        var result = _repository.GetAll().ToList();

        // Assert
        Assert.Single(result);
        Assert.Contains(result, b => b.Id == activeBook.Id);
        Assert.DoesNotContain(result, b => b.Id == deletedBook.Id);
    }
}
