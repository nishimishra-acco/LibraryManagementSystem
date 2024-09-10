using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Repositories
{
    public class InMemoryLoanRepository : ILoanRepository
    {
        private readonly List<LoanDto> _loans = new();

        public void Add(LoanDto loan) => _loans.Add(loan);

        public LoanDto GetByBookId(Guid bookId) => _loans.FirstOrDefault(l => l.BookId == bookId);

        public IEnumerable<LoanDto> GetAll() => _loans;
    }

}
