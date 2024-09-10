namespace LibraryManagementSystem.Service.Models
{
    public class LoanDto
    {
        public Guid BookId { get; set; }
        public int UserId { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime? ReturnDate { get; set; }
    }
}
