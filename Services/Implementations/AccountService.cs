using HomeBanking.Models;
using HomeBanking.Repositories;

namespace HomeBanking.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        private string GenerateAccountNumber()
        {
            Random random = new Random();
            int randomNumber = random.Next(10000000, 99999999); // Genera un número de 8 dígitos
            return $"VIN-{randomNumber}";
        }

        public string GenerateUniqueAccountNumber()
        {
            string accountNumber;
            do
            {
                accountNumber = GenerateAccountNumber();
            } while (_accountRepository.IsAccountNumberInUse(accountNumber) != null);

            return accountNumber;
        }

        public void Save(Account account)
        {
            _accountRepository.Save(account);
        }

        public Account FindAccountByNumber(string number)
        {
            return _accountRepository.FindAccountByNumber(number);
        }
        public Account FindAccountById(long id)
        {
            return _accountRepository.FindAccountById(id);
        }

        public IEnumerable<Account> FindAccountsByClient(long clientId)
        {
            return _accountRepository.FindAccountsByClient(clientId);
        }

        public IEnumerable<Account> GetAllAccounts()
        {
            return _accountRepository.GetAllAccounts();
        }

        public Account CreateAccount(Client client)
        {
            var newAccount = new Account
            {
                CreationDate = DateTime.Now,
                Balance = 0,
                ClientId = client.Id,
                Transactions = new List<Transaction>(),
                Number = GenerateUniqueAccountNumber()
            };

            Save(newAccount);

            return newAccount;
        }

        public Account CreateAccForExistingClient(Client client)
        {
            if (client.Accounts.Count >= 3)
            {
                throw new Exception("Alcanzado el límite de cuentas.");
            }

            var newAccount = new Account
            {
                CreationDate = DateTime.Now,
                Balance = 0,
                ClientId = client.Id,
                Transactions = new List<Transaction>(),
                Number = GenerateUniqueAccountNumber()
            };

            Save(newAccount);

            return newAccount;
        }

        public void Update(Account account)
        {
            _accountRepository.UpdateAccount(account);
        }
    }
}
