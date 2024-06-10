using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface IAccountService
    {
        string GenerateUniqueAccountNumber();
        Account FindAccountById(long id);
        Account FindAccountByNumber(string number);
        Account CreateAccount(Client client);
        Account CreateAccForExistingClient(Client client);
        IEnumerable<Account> GetAllAccounts();
        IEnumerable<Account> FindAccountsByClient(long clientId);
        void Save(Account account);
        void Update(Account account);
    }
}
