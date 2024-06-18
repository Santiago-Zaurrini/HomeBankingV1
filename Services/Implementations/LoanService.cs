using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Models;
using HomeBanking.Repositories;
using System.Net;

namespace HomeBanking.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _loanRepository;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly IClientService _clientService;
        private readonly IClientLoanRepository _clientLoanRepository;

        public LoanService(ILoanRepository loanRepository, IAccountService accountService, ITransactionService transactionService,
            IClientService clientService, IClientLoanRepository clientLoanRepository)
        {
            _accountService = accountService;
            _loanRepository = loanRepository;
            _transactionService = transactionService;
            _clientService = clientService;
            _clientLoanRepository = clientLoanRepository;
        }

        public void CreateLoan(LoanAplicationDTO loanAplication, string clientEmail)
        {
            try
            {
                //validacion datos
                if (loanAplication.LoanId == 0 || string.IsNullOrEmpty(loanAplication.ToAccountNumber) ||
                    string.IsNullOrEmpty(loanAplication.Payments) || loanAplication.Amount <= 0)
                        throw new CustomException("Alguno de los datos está vacío / monto es 0", 403);

                //existe el préstamo
                Loan loan = _loanRepository.GetLoanById(loanAplication.LoanId);
                if (loan == null)
                    throw new CustomException("El préstamo solicitado no existe", 403);

                //validar monto contra monto máximo
                if (loanAplication.Amount > loan.MaxAmount)
                    throw new CustomException("El monto solicitado es mayor al monto máximo del préstamo", 403);

                //validar cantidad cuotas
                List<string> availablePayments = loan.Payments.Split(',').ToList();
                if (!availablePayments.Contains(loanAplication.Payments))
                    throw new CustomException("La cantidad de cuotas solicitadas no es válida", 403);

                //cliente que pide el prestamo
                Client client = _clientService.GetCurrent(clientEmail);
                if (client == null)
                    throw new CustomException("Cliente no encontrado", 403);

                //cuenta destino para el prestamo
                Account account = _accountService.FindAccountByNumber(loanAplication.ToAccountNumber);
                if (account == null)
                    throw new CustomException("La cuenta de destino no existe", 403);

                //validar dueño de cuenta
                if (account.ClientId != client.Id)
                    throw new CustomException("La cuenta de destino no es del cliente autenticado", 403);

                double newLoanAmount = loanAplication.Amount * 1.20;
                ClientLoan clientLoan = new ClientLoan
                {
                    Amount = newLoanAmount,
                    Payments = loanAplication.Payments,
                    ClientId = client.Id,
                    LoanId = loan.Id
                };
                _clientLoanRepository.Save(clientLoan);

                //transacción a favor (credit) en cuenta destino
                Transaction transaction = new Transaction
                {
                    Type = "CREDIT",
                    Amount = loanAplication.Amount,
                    Description = loan.Name + " - Préstamo aprobado",
                    Date = DateTime.Now,
                    AccountId = account.Id
                };
                _transactionService.Save(transaction);


                account.Balance += loanAplication.Amount;
                _accountService.Update(account);
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message, ex.StatusCode);
            }
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            return _loanRepository.GetAllLoans();
        }

        public Loan GetLoanById(long id)
        {
            return _loanRepository.GetLoanById(id);
        }

        public void Save(Loan loan)
        {
            _loanRepository.Save(loan);
        }
    }
}
