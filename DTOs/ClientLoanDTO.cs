using HomeBanking.Models;

namespace HomeBanking.DTOs
{
    public class ClientLoanDTO
    {
        public long Id { get; set; }
        public long LoanId { get; set; }
        public string Name { get; set; }
        public double Amount { get; set; }
        public string Payments { get; set; }

        public ClientLoanDTO(ClientLoan clientLoan)
        {
            Id = clientLoan.Id;
            LoanId = clientLoan.LoanId;
            Name = clientLoan.Loan.Name;
            Amount = clientLoan.Amount;
            Payments = clientLoan.Payments;
        }
    }
}
