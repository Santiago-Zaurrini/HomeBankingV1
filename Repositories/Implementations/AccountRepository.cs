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

        public Account FindAccountByNumber(string number)
        {
            return FindByCondition(ac => ac.Number == number).
                Include(ac => ac.Transactions)
                .FirstOrDefault();
        }
        public Account IsAccountNumberInUse(string accountNumber)
        {
            return FindByCondition(a => a.Number == accountNumber)
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

        public void Save(Account account)
        {
            Create(account);
            SaveChanges();
        }

        public void UpdateAccount(Account account)
        {
            Update(account);
            SaveChanges();
            RepositoryContext.ChangeTracker.Clear();
        }
    }
}
