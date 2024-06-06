using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface ITransactionService
    {
        IEnumerable<Transaction> GetAllTransactions();
        Transaction GetTransactionById(long id);
        void Transfer(string originNumber, string destinationNumber, double amount, string description);
    }
}
