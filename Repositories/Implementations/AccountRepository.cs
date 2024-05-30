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
    }
}
