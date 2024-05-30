using HomeBanking.Models;

namespace HomeBanking.Repositories.Implementations
{
    public class TransactionRepository : RepositoryBase<Transaction>, ITransactionRepository
    {
        public TransactionRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Transaction GetTransactionById(long id)
        {
            return FindByCondition(tr => tr.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return FindAll()
                .ToList();
        }

        public void Save(Transaction transaction)
        {
            Create(transaction);
            SaveChanges();
        }
    }
}
