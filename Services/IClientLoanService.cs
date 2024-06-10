using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface IClientLoanService
    {
        void Save(ClientLoan clientLoan);
    }
}
