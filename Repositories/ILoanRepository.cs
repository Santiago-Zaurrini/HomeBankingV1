using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAllLoans();
        Loan GetLoanById(long id);
        void Save(Loan loan);
    }
}
