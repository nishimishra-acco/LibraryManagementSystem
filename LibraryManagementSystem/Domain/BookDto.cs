﻿using System.Reflection;

//unused 'using' don't need it

// the EntityDto should be in another class.
// why is there not at least a blank line between these two objects?
namespace LibraryManagementSystem.Service.Models
{
    public class BookDto : EntityDto
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public bool IsCheckedOut { get; set; }
    }
    public abstract class EntityDto
    {
        public Guid Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
    }
}
