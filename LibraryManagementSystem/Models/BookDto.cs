using System.Reflection;

namespace LibraryManagementSystem.Service.Models
{
    public class BookDto : EntityDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public BookDto() { }
        public BookDto(string title, string author, string description)
        {
            Title = title;
            Author = author;
            Description = description;
            Id = Guid.NewGuid();
            IsDeleted = false;
            IsCheckedOut = false;
            AddedBy = 1;
        }
    }
    public abstract class EntityDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCheckedOut { get; set; }
        public int AddedBy { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
