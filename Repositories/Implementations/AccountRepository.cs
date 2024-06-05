using HomeBanking.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking.Repositories.Implementations
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Account FindAccountById(long id)
        {
            return FindByCondition(ac => ac.Id == id).
                Include(ac => ac.Transactions)
                .FirstOrDefault();
        }

        public IEnumerable<Account> FindAccountsByClient(long clientId)
        {
            return FindByCondition(ac => ac.ClientId == clientId)
                .Include(ac => ac.Transactions)
                .ToList();
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return FindAll()
                .Include(ac => ac.Transactions)
                .ToList();
        }

        public Account IsAccountNumberInUse(string accountNumber)
        {
            return FindByCondition(a => a.Number == accountNumber)
                .FirstOrDefault();
        }

        public void Save(Account account)
        {
            Create(account);
            SaveChanges();
        }

    }
}
