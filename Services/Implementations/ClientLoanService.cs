using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Models;
using HomeBanking.Repositories;
using System.Net;

namespace HomeBanking.Services.Implementations
{
    public class ClientLoanService : IClientLoanService
    {
        private readonly IClientLoanRepository _cloanRepository;
        public ClientLoanService(IClientLoanRepository cloanRepository)
        {
            _cloanRepository = cloanRepository;
        }

        public void Save(ClientLoan clientLoan)
        {
            _cloanRepository.Save(clientLoan);
        }
    }
}
