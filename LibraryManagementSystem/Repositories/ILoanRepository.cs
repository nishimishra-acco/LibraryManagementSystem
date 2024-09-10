
using LibraryManagementSystem.Service.Models;

namespace LibraryManagementSystem.Service.Repositories
{
    public interface ILoanRepository
    {
        void Add(LoanDto loan);
        LoanDto GetByBookId(Guid bookId);
        IEnumerable<LoanDto> GetAll();
    }
}
