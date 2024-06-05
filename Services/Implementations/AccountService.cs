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
    }
}
