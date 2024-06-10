using HomeBanking.DTOs;
using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetAllLoans();
        Loan GetLoanById(long id);
        void Save(Loan loan);
        void CreateLoan(LoanAplicationDTO loanAplication, string clientEmail);
    }
}
