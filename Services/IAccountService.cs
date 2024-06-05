using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface IAccountService
    {
        string GenerateUniqueAccountNumber();
        void Save(Account account);
    }
}
