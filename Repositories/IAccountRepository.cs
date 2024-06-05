using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        Account FindAccountById(long id);
        IEnumerable<Account> FindAccountsByClient(long clientId);
        public void Save(Account account);
        Account IsAccountNumberInUse(string accountNumber);

    }
}
