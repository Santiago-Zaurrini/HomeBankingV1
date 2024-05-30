using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface ITransactionRepository
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransactionById(long id);
        void Save(Transaction transaction);
    }
}
