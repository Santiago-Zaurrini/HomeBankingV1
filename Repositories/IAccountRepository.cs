using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface IAccountRepository
    {
        Account FindAccountById(long id);
        Account FindAccountByNumber(string number);
        Account IsAccountNumberInUse(string accountNumber);
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Account> FindAccountsByClient(long clientId);
        void Save(Account account);
        void UpdateAccount(Account account);

    }
}
