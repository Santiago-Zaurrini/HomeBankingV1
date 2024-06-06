using HomeBanking.Exceptions;
using HomeBanking.Models;
using HomeBanking.Repositories;
using System.Net;

namespace HomeBanking.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public IEnumerable<Transaction> GetAllTransactions()
        {
            return _transactionRepository.GetAllTransactions();
        }

        public Transaction GetTransactionById(long id)
        {
            return _transactionRepository.GetTransactionById(id);
        }

        public void Transfer(string originNumber, string destinationNumber, double amount, string description)
        {
            var originAcc = _accountRepository.FindAccountByNumber(originNumber);
            var destinationAcc = _accountRepository.FindAccountByNumber(destinationNumber);

            if (originAcc == null)
                throw new CustomException("Cuenta origen no encontrada.", HttpStatusCode.Forbidden);
            if (destinationAcc == null)
                throw new CustomException("Cuenta destino no encontrada.", HttpStatusCode.Forbidden);
            if (originNumber == destinationNumber)
                throw new CustomException("No se puede realizar la operación en la misma cuenta.", HttpStatusCode.BadRequest);
            if (amount > originAcc.Balance)
                throw new CustomException("Saldo insuficiente para la operación.", HttpStatusCode.BadRequest);

            originAcc.Balance -= amount;
            _accountRepository.UpdateAccount(originAcc);

            destinationAcc.Balance += amount;
            _accountRepository.UpdateAccount(destinationAcc);

            var originTrans = new Transaction
            {
                Type = "DEBIT",
                Amount = -amount,
                Description = description,
                Date = DateTime.Now,
                AccountId = originAcc.Id,
            };
            _transactionRepository.Save(originTrans);

            var destinationTrans = new Transaction
            {
                Type = "CREDIT",
                Amount = amount,
                Description = description,
                Date = DateTime.Now,
                AccountId = destinationAcc.Id,
            };

            _transactionRepository.Save(destinationTrans);
        }
    }
}
